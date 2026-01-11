using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StaffWeb.Data;
using StaffWeb.Models;
using StaffWeb.Utils;

namespace StaffWeb.Areas.Director.Controllers;

[Area("Director")]
[Authorize(Roles = "Director")]
public class ReportsController : Controller
{
    private readonly AppDbContext _db;
    private const string PaidStatus = "Đã thanh toán";
    public ReportsController(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IActionResult> Revenue(DateTime? fromDate, DateTime? toDate, string? branchId, string? period)
    {
        var vm = await BuildRevenueViewModel(fromDate, toDate, branchId, period);
        return View(vm);
    }

    public async Task<IActionResult> Doctors(DateTime? fromDate, DateTime? toDate, string? branchId)
    {
        var vm = new DirectorDoctorViewModel
        {
            FromDate = fromDate,
            ToDate = toDate,
            BranchId = branchId,
            Branches = await LoadBranches()
        };
        vm.Items = await BuildDoctorStats(branchId, fromDate, toDate);
        return View(vm);
    }

    public async Task<IActionResult> Visits(DateTime? fromDate, DateTime? toDate, string? branchId)
    {
        var vm = new DirectorVisitViewModel
        {
            FromDate = fromDate,
            ToDate = toDate,
            BranchId = branchId,
            Branches = await LoadBranches(),
            Items = await BuildVisitStats(branchId, fromDate, toDate)
        };
        return View(vm);
    }

    public async Task<IActionResult> Products(DateTime? fromDate, DateTime? toDate, string? branchId, int top = 10)
    {
        var vm = new DirectorProductViewModel
        {
            FromDate = fromDate,
            ToDate = toDate,
            BranchId = branchId,
            Top = top,
            Branches = await LoadBranches()
        };

        vm.Items = await BuildProductStats(branchId, fromDate, toDate, top);
        vm.SystemRevenue = await BuildSystemProductRevenue(fromDate, toDate);
        return View(vm);
    }

    public async Task<IActionResult> CompareBranches(DateTime? fromDate, DateTime? toDate, string[]? branchIds)
    {
        var vm = new DirectorBranchCompareViewModel
        {
            FromDate = fromDate,
            ToDate = toDate,
            Branches = await LoadBranches(),
            SelectedBranchIds = branchIds?.Where(x => !string.IsNullOrWhiteSpace(x)).Distinct().ToList()
                ?? new List<string>()
        };

        if (!fromDate.HasValue || !toDate.HasValue || fromDate > toDate || vm.SelectedBranchIds.Count == 0)
        {
            return View(vm);
        }

        var from = fromDate.Value.Date;
        var toExclusive = toDate.Value.Date.AddDays(1);
        var selectedIds = vm.SelectedBranchIds;

        var branchMap = await _db.chinhanhs.AsNoTracking()
            .Where(x => selectedIds.Contains(x.macn))
            .ToDictionaryAsync(x => x.macn, x => x.tencn);

        var revenueRaw = await _db.hoadons.AsNoTracking()
            .Where(x => x.trangthai == PaidStatus
                && x.ngaylap >= from && x.ngaylap < toExclusive
                && x.macn != null
                && selectedIds.Contains(x.macn))
            .GroupBy(x => x.macn)
            .Select(g => new
            {
                macn = g.Key!,
                sohoadon = g.Count(),
                tongdoanhthu = g.Sum(x => x.thanhtien ?? (x.tongtien - x.khuyenmai))
            })
            .ToListAsync();

        vm.Revenue = revenueRaw
            .Select(g => new RevenueByBranch
            {
                macn = g.macn,
                tencn = branchMap.TryGetValue(g.macn, out var name) ? name : g.macn,
                sohoadon = g.sohoadon,
                tongdoanhthu = g.tongdoanhthu
            })
            .OrderByDescending(x => x.tongdoanhthu)
            .ToList();

        var productRaw = await _db.chitietmuasanphams.AsNoTracking()
            .Join(_db.hoadons.AsNoTracking(),
                ct => ct.mahd,
                hd => hd.mahd,
                (ct, hd) => new { ct, hd })
            .Where(x => x.hd.trangthai == PaidStatus
                && x.hd.ngaylap >= from && x.hd.ngaylap < toExclusive
                && x.hd.macn != null
                && selectedIds.Contains(x.hd.macn))
            .GroupBy(x => x.hd.macn)
            .Select(g => new
            {
                BranchId = g.Key!,
                TotalRevenue = g.Sum(x => x.ct.thanhtien),
                TotalQuantity = g.Sum(x => x.ct.soluong)
            })
            .ToListAsync();

        vm.ProductRevenue = productRaw
            .Select(g => new BranchProductRevenueRow
            {
                BranchId = g.BranchId,
                BranchName = branchMap.TryGetValue(g.BranchId, out var name) ? name : g.BranchId,
                TotalRevenue = g.TotalRevenue,
                TotalQuantity = g.TotalQuantity
            })
            .OrderByDescending(x => x.TotalRevenue)
            .ToList();

        return View(vm);
    }

    private async Task<List<BranchOption>> LoadBranches()
    {
        return await _db.chinhanhs.AsNoTracking()
            .OrderBy(x => x.tencn)
            .Select(x => new BranchOption { Id = x.macn, Name = x.tencn })
            .ToListAsync();
    }

    private async Task<DirectorRevenueViewModel> BuildRevenueViewModel(DateTime? fromDate, DateTime? toDate, string? branchId, string? period)
    {
        var vm = new DirectorRevenueViewModel
        {
            FromDate = fromDate,
            ToDate = toDate,
            BranchId = branchId,
            Period = NormalizePeriod(period),
            Branches = await LoadBranches()
        };

        if (!fromDate.HasValue || !toDate.HasValue || fromDate > toDate)
        {
            return vm;
        }

        var from = fromDate.Value.Date;
        var to = toDate.Value.Date.AddDays(1).AddSeconds(-1);

        var previousTimeout = _db.Database.GetCommandTimeout();
        _db.Database.SetCommandTimeout(120);
        try
        {
            var baseQuery = _db.hoadons.AsNoTracking()
                .Where(x => x.trangthai == PaidStatus && x.ngaylap >= from && x.ngaylap <= to);

            if (string.IsNullOrWhiteSpace(branchId))
            {
                var branchMap = await _db.chinhanhs.AsNoTracking()
                    .ToDictionaryAsync(x => x.macn, x => x.tencn);

                var grouped = await baseQuery
                    .Where(x => x.macn != null)
                    .GroupBy(x => x.macn)
                    .Select(g => new
                    {
                        macn = g.Key!,
                        sohoadon = g.Count(),
                        tongdoanhthu = g.Sum(x => x.thanhtien ?? (x.tongtien - x.khuyenmai))
                    })
                    .ToListAsync();

                vm.Items = grouped
                    .Select(g => new RevenueByBranch
                    {
                        macn = g.macn,
                        tencn = branchMap.TryGetValue(g.macn, out var name) ? name : g.macn,
                        sohoadon = g.sohoadon,
                        tongdoanhthu = g.tongdoanhthu
                    })
                    .OrderByDescending(x => x.tongdoanhthu)
                    .ToList();

                vm.BranchName = "Tất cả chi nhánh";
                vm.BranchRevenue = vm.Items.Sum(x => x.tongdoanhthu);
                vm.Sources = await BuildRevenueSources(null, from, to);
            }

            if (!string.IsNullOrWhiteSpace(branchId))
            {
                var branch = await _db.chinhanhs.AsNoTracking()
                    .Where(x => x.macn == branchId)
                    .Select(x => new { x.macn, x.tencn })
                    .FirstOrDefaultAsync();

                var branchQuery = baseQuery
                    .Where(x => x.macn == branchId)
                    .Select(x => x.thanhtien ?? (x.tongtien - x.khuyenmai));
                vm.BranchName = branch?.tencn;
                vm.BranchRevenue = await branchQuery.SumAsync();

                vm.Sources = await BuildRevenueSources(branchId, from, to);
            }

            var trendQuery = baseQuery;
            if (!string.IsNullOrWhiteSpace(branchId))
            {
                trendQuery = trendQuery.Where(x => x.macn == branchId);
            }
            else
            {
                trendQuery = trendQuery.Where(x => x.macn != null);
            }

            var trendRaw = await trendQuery
                .Select(x => new
                {
                    ngay = x.ngaylap,
                    tong = x.thanhtien ?? (x.tongtien - x.khuyenmai)
                })
                .ToListAsync();

            vm.Trend = trendRaw
                .GroupBy(x => GetPeriodStart(x.ngay, vm.Period))
                .Select(g => new ManagerRevenueTrendRow
                {
                    ngay = g.Key,
                    tongdoanhthu = g.Sum(x => x.tong)
                })
                .OrderBy(x => x.ngay)
                .ToList();

            return vm;
        }
        finally
        {
            _db.Database.SetCommandTimeout(previousTimeout);
        }
    }
    private async Task<List<RevenueSourceSlice>> BuildRevenueSources(string? branchId, DateTime from, DateTime to)
    {
        var examQuery = _db.chitietkhambenhs.AsNoTracking()
            .Where(x => x.ngaysudung >= from && x.ngaysudung <= to)
            .Join(_db.nhanviens.AsNoTracking(),
                kb => kb.mabs,
                nv => nv.manv,
                (kb, nv) => new { kb, nv });

        var vaccQuery = _db.chitiettiemphongs.AsNoTracking()
            .Where(x => x.ngaytiem >= from && x.ngaytiem <= to)
            .Join(_db.nhanviens.AsNoTracking(),
                tp => tp.mabs,
                nv => nv.manv,
                (tp, nv) => new { tp, nv });

        if (!string.IsNullOrWhiteSpace(branchId))
        {
            examQuery = examQuery.Where(x => x.nv.macn == branchId);
            vaccQuery = vaccQuery.Where(x => x.nv.macn == branchId);
        }

        var examRevenue = await examQuery
            .Join(_db.dichvus.AsNoTracking(),
                kb => kb.kb.madv,
                dv => dv.madv,
                (kb, dv) => dv.gia)
            .SumAsync();

        var vaccRevenue = await vaccQuery
            .Join(_db.dichvus.AsNoTracking(),
                tp => tp.tp.madv,
                dv => dv.madv,
                (tp, dv) => dv.gia)
            .SumAsync();

        var productRevenue = await _db.chitietmuasanphams.AsNoTracking()
            .Join(_db.hoadons.AsNoTracking(),
                ct => ct.mahd,
                hd => hd.mahd,
                (ct, hd) => new { ct, hd })
            .Where(x => x.hd.trangthai == PaidStatus && x.hd.ngaylap >= from && x.hd.ngaylap <= to)
            .Where(x => string.IsNullOrWhiteSpace(branchId)
                ? x.hd.macn != null
                : x.hd.macn == branchId)
            .SumAsync(x => x.ct.thanhtien);

        return new List<RevenueSourceSlice>
        {
            new() { Name = "Kham benh", Value = examRevenue },
            new() { Name = "Tiem phong", Value = vaccRevenue },
            new() { Name = "Ban san pham", Value = productRevenue }
        };
    }
private async Task<List<DoctorStat>> BuildDoctorStats(string? branchId, DateTime? fromDate, DateTime? toDate)
    {
        if (!fromDate.HasValue || !toDate.HasValue || fromDate > toDate)
        {
            return new List<DoctorStat>();
        }

        var previousTimeout = _db.Database.GetCommandTimeout();
        _db.Database.SetCommandTimeout(120);
        try
        {
        var doctorsQuery = _db.nhanviens.AsNoTracking()
            .Where(x => x.loainv == "Bác sĩ thú y");
        if (!string.IsNullOrWhiteSpace(branchId))
        {
            doctorsQuery = doctorsQuery.Where(x => x.macn == branchId);
        }

        var doctors = await doctorsQuery
            .Select(x => new { x.manv, x.hoten })
            .ToListAsync();
        var doctorIds = doctors.Select(x => x.manv).ToList();
        if (doctorIds.Count == 0)
        {
            return new List<DoctorStat>();
        }

        var from = fromDate.Value.Date;
        var to = toDate.Value.Date.AddDays(1).AddSeconds(-1);

        var examCounts = await _db.chitietkhambenhs.AsNoTracking()
            .Where(x => x.ngaysudung >= from && x.ngaysudung <= to)
            .Join(doctorsQuery,
                kb => kb.mabs,
                nv => nv.manv,
                (kb, nv) => kb.mabs)
            .GroupBy(x => x)
            .Select(g => new { manv = g.Key, count = g.Count() })
            .ToListAsync();

        var vaccCounts = await _db.chitiettiemphongs.AsNoTracking()
            .Where(x => x.ngaytiem >= from && x.ngaytiem <= to)
            .Join(doctorsQuery,
                tp => tp.mabs,
                nv => nv.manv,
                (tp, nv) => tp.mabs)
            .GroupBy(x => x)
            .Select(g => new { manv = g.Key, count = g.Count() })
            .ToListAsync();

        var examRevenue = await _db.chitietkhambenhs.AsNoTracking()
            .Where(x => x.ngaysudung >= from && x.ngaysudung <= to)
            .Join(doctorsQuery,
                kb => kb.mabs,
                nv => nv.manv,
                (kb, nv) => new { kb.mabs, kb.madv })
            .Join(_db.dichvus.AsNoTracking(),
                kb => kb.madv,
                dv => dv.madv,
                (kb, dv) => new { kb.mabs, dv.gia })
            .GroupBy(x => x.mabs)
            .Select(g => new { manv = g.Key, total = g.Sum(x => x.gia) })
            .ToListAsync();

        var vaccRevenue = await _db.chitiettiemphongs.AsNoTracking()
            .Where(x => x.ngaytiem >= from && x.ngaytiem <= to)
            .Join(doctorsQuery,
                tp => tp.mabs,
                nv => nv.manv,
                (tp, nv) => new { tp.mabs, tp.madv })
            .Join(_db.dichvus.AsNoTracking(),
                tp => tp.madv,
                dv => dv.madv,
                (tp, dv) => new { tp.mabs, dv.gia })
            .GroupBy(x => x.mabs)
            .Select(g => new { manv = g.Key, total = g.Sum(x => x.gia) })
            .ToListAsync();

        return doctors.Select(d =>
        {
            var exam = examCounts.FirstOrDefault(x => x.manv == d.manv)?.count ?? 0;
            var vacc = vaccCounts.FirstOrDefault(x => x.manv == d.manv)?.count ?? 0;
            var examTotal = examRevenue.FirstOrDefault(x => x.manv == d.manv)?.total ?? 0;
            var vaccTotal = vaccRevenue.FirstOrDefault(x => x.manv == d.manv)?.total ?? 0;
            return new DoctorStat
            {
                manv = d.manv,
                hoten = d.hoten,
                solankham = exam,
                solantiem = vacc,
                doanhthu = examTotal + vaccTotal
            };
        }).OrderByDescending(x => x.doanhthu).ToList();
        }
        finally
        {
            _db.Database.SetCommandTimeout(previousTimeout);
        }
    }

    private async Task<List<DirectorVisitRow>> BuildVisitStats(string? branchId, DateTime? fromDate, DateTime? toDate)
    {
        if (!fromDate.HasValue || !toDate.HasValue || fromDate > toDate)
        {
            return new List<DirectorVisitRow>();
        }

        var from = fromDate.Value.Date;
        var to = toDate.Value.Date.AddDays(1).AddSeconds(-1);

        var examQuery = _db.chitietkhambenhs.AsNoTracking()
            .Where(x => x.ngaysudung >= from && x.ngaysudung <= to)
            .Join(_db.nhanviens.AsNoTracking(),
                kb => kb.mabs,
                nv => nv.manv,
                (kb, nv) => new { kb, nv });

        var vaccQuery = _db.chitiettiemphongs.AsNoTracking()
            .Where(x => x.ngaytiem >= from && x.ngaytiem <= to)
            .Join(_db.nhanviens.AsNoTracking(),
                tp => tp.mabs,
                nv => nv.manv,
                (tp, nv) => new { tp, nv });

        if (!string.IsNullOrWhiteSpace(branchId))
        {
            examQuery = examQuery.Where(x => x.nv.macn == branchId);
            vaccQuery = vaccQuery.Where(x => x.nv.macn == branchId);
        }

        var examCounts = await examQuery
            .GroupBy(x => x.kb.ngaysudung.Date)
            .Select(g => new { Date = g.Key, Count = g.Count() })
            .ToListAsync();

        var vaccCounts = await vaccQuery
            .GroupBy(x => x.tp.ngaytiem.Date)
            .Select(g => new { Date = g.Key, Count = g.Count() })
            .ToListAsync();

        var dates = examCounts.Select(x => x.Date)
            .Union(vaccCounts.Select(x => x.Date))
            .Distinct()
            .OrderByDescending(x => x)
            .ToList();

        return dates.Select(date => new DirectorVisitRow
        {
            Date = date,
            ExamCount = examCounts.FirstOrDefault(x => x.Date == date)?.Count ?? 0,
            VaccinationCount = vaccCounts.FirstOrDefault(x => x.Date == date)?.Count ?? 0
        }).ToList();
    }

    private async Task<List<ProductStat>> BuildProductStats(string? branchId, DateTime? fromDate, DateTime? toDate, int top)
    {
        if (!fromDate.HasValue || !toDate.HasValue || fromDate > toDate)
        {
            return new List<ProductStat>();
        }

        var from = fromDate.Value.Date;
        var to = toDate.Value.Date.AddDays(1).AddSeconds(-1);

        var query = _db.chitietmuasanphams.AsNoTracking()
            .Join(_db.hoadons.AsNoTracking(),
                ct => ct.mahd,
                hd => hd.mahd,
                (ct, hd) => new { ct, hd })
            .Where(x => x.hd.trangthai == PaidStatus && x.hd.ngaylap >= from && x.hd.ngaylap <= to);

        if (!string.IsNullOrWhiteSpace(branchId))
        {
            query = query.Where(x => x.hd.macn == branchId);
        }

        return await query
            .Join(_db.sanphams.AsNoTracking(),
                x => x.ct.masp,
                sp => sp.masp,
                (x, sp) => new { x.ct, sp })
            .GroupBy(x => new { x.sp.masp, x.sp.tensp })
            .Select(g => new ProductStat
            {
                masp = g.Key.masp,
                tensp = g.Key.tensp,
                tongsoluong = g.Sum(x => x.ct.soluong),
                tongdoanhthu = g.Sum(x => x.ct.thanhtien)
            })
            .OrderByDescending(x => x.tongdoanhthu)
            .Take(top)
            .ToListAsync();
    }

    private async Task<decimal?> BuildSystemProductRevenue(DateTime? fromDate, DateTime? toDate)
    {
        if (!fromDate.HasValue || !toDate.HasValue || fromDate > toDate)
        {
            return null;
        }

        var from = fromDate.Value.Date;
        var to = toDate.Value.Date.AddDays(1).AddSeconds(-1);

        return await _db.chitietmuasanphams.AsNoTracking()
            .Join(_db.hoadons.AsNoTracking(),
                ct => ct.mahd,
                hd => hd.mahd,
                (ct, hd) => new { ct, hd })
            .Where(x => x.hd.trangthai == PaidStatus && x.hd.ngaylap >= from && x.hd.ngaylap <= to)
            .SumAsync(x => x.ct.thanhtien);
    }

    private async Task<List<BranchProductRevenueRow>> BuildBranchProductRevenue(List<string> branchIds, DateTime? fromDate, DateTime? toDate)
    {
        if (branchIds.Count == 0 || !fromDate.HasValue || !toDate.HasValue || fromDate > toDate)
        {
            return new List<BranchProductRevenueRow>();
        }

        var from = fromDate.Value.Date;
        var to = toDate.Value.Date.AddDays(1).AddSeconds(-1);

        var query = _db.chitietmuasanphams.AsNoTracking()
            .Join(_db.hoadons.AsNoTracking(),
                ct => ct.mahd,
                hd => hd.mahd,
                (ct, hd) => new { ct, hd })
            .Where(x => x.hd.trangthai == PaidStatus
                && x.hd.ngaylap >= from && x.hd.ngaylap <= to
                && x.hd.macn != null
                && branchIds.Contains(x.hd.macn));

        return await query
            .GroupBy(x => x.hd.macn)
            .Join(_db.chinhanhs.AsNoTracking(),
                g => g.Key,
                cn => cn.macn,
                (g, cn) => new BranchProductRevenueRow
                {
                    BranchId = cn.macn,
                    BranchName = cn.tencn,
                    TotalRevenue = g.Sum(x => x.ct.thanhtien),
                    TotalQuantity = g.Sum(x => x.ct.soluong)
                })
            .OrderByDescending(x => x.TotalRevenue)
            .ToListAsync();
    }

    private static string NormalizePeriod(string? period)
    {
        return period?.Trim().ToLowerInvariant() switch
        {
            "week" => "week",
            "month" => "month",
            "year" => "year",
            _ => "day"
        };
    }

    private static DateTime GetPeriodStart(DateTime date, string period)
    {
        var day = date.Date;
        return period switch
        {
            "week" => day.AddDays(-((7 + (int)day.DayOfWeek - (int)DayOfWeek.Monday) % 7)),
            "month" => new DateTime(day.Year, day.Month, 1),
            "year" => new DateTime(day.Year, 1, 1),
            _ => day
        };
    }
}






















