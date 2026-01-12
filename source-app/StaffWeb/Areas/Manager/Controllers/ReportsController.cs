using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.IO;
using ClosedXML.Excel;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using StaffWeb.Data;
using StaffWeb.Models;

namespace StaffWeb.Areas.Manager.Controllers;

[Area("Manager")]
[Authorize(Roles = "Manager")]
public class ReportsController : Controller
{
    private readonly AppDbContext _db;

    public ReportsController(AppDbContext db)
    {
        _db = db;
        QuestPDF.Settings.License = LicenseType.Community;
    }

    public async Task<IActionResult> Revenue(DateTime? fromDate, DateTime? toDate, string? period)
    {
        var macn = User.FindFirstValue("MaChiNhanh");
        var vm = await BuildRevenueViewModel(fromDate, toDate, macn, period);
        return View(vm);
    }

    public async Task<IActionResult> Doctors(DateTime? fromDate, DateTime? toDate)
    {
        var macn = User.FindFirstValue("MaChiNhanh");
        var vm = new ManagerDoctorViewModel
        {
            FromDate = fromDate,
            ToDate = toDate,
            Items = await BuildDoctorStats(macn, fromDate, toDate)
        };

        return View(vm);
    }

    public async Task<IActionResult> Products(DateTime? fromDate, DateTime? toDate, int top = 10)
    {
        var macn = User.FindFirstValue("MaChiNhanh");
        var vm = new ManagerProductViewModel
        {
            FromDate = fromDate,
            ToDate = toDate,
            Top = top,
            Items = await BuildProductStats(macn, fromDate, toDate, top),
            Categories = await BuildProductCategories(macn, fromDate, toDate)
        };
        return View(vm);
    }

    public async Task<IActionResult> Visits(DateTime? fromDate, DateTime? toDate, string? period)
    {
        var macn = User.FindFirstValue("MaChiNhanh");
        var normalized = NormalizePeriod(period);
        var vm = new ManagerVisitViewModel
        {
            FromDate = fromDate,
            ToDate = toDate,
            Period = normalized,
            Items = await BuildVisitStats(macn, fromDate, toDate, normalized)
        };

        return View(vm);
    }

    public async Task<IActionResult> RevenueExcel(DateTime? fromDate, DateTime? toDate, string? period)
    {
        var macn = User.FindFirstValue("MaChiNhanh");
        var vm = await BuildRevenueViewModel(fromDate, toDate, macn, period);
        if (vm.Items.Count == 0)
        {
            return BadRequest("Khong co du lieu.");
        }

        using var workbook = new XLWorkbook();
        var ws = workbook.Worksheets.Add("DoanhThu");
        ws.Cell(1, 1).Value = "Ma CN";
        ws.Cell(1, 2).Value = "Ten chi nhanh";
        ws.Cell(1, 3).Value = "So hoa don";
        ws.Cell(1, 4).Value = "Tong doanh thu";

        var row = 2;
        foreach (var item in vm.Items)
        {
            ws.Cell(row, 1).Value = item.macn;
            ws.Cell(row, 2).Value = item.tencn;
            ws.Cell(row, 3).Value = item.sohoadon;
            ws.Cell(row, 4).Value = item.tongdoanhthu;
            row++;
        }

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        stream.Position = 0;
        return File(stream.ToArray(),
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            "doanh-thu.xlsx");
    }

    public async Task<IActionResult> RevenuePdf(DateTime? fromDate, DateTime? toDate, string? period)
    {
        var macn = User.FindFirstValue("MaChiNhanh");
        var vm = await BuildRevenueViewModel(fromDate, toDate, macn, period);
        if (vm.Items.Count == 0)
        {
            return BadRequest("Khong co du lieu.");
        }

        var pdf = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(30);
                page.Size(PageSizes.A4);
                page.Content().Column(col =>
                {
                    col.Item().Text("Bao cao doanh thu").FontSize(18).Bold();
                    if (vm.FromDate.HasValue && vm.ToDate.HasValue)
                    {
                        col.Item().Text($"Tu ngay: {vm.FromDate:dd/MM/yyyy} - Den ngay: {vm.ToDate:dd/MM/yyyy}");
                    }
                    col.Item().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(2);
                            columns.RelativeColumn(4);
                            columns.RelativeColumn(2);
                            columns.RelativeColumn(3);
                        });
                        table.Header(header =>
                        {
                            header.Cell().Element(CellStyle).Text("Ma CN");
                            header.Cell().Element(CellStyle).Text("Ten chi nhanh");
                            header.Cell().Element(CellStyle).Text("So hoa don");
                            header.Cell().Element(CellStyle).Text("Tong doanh thu");
                        });
                        foreach (var item in vm.Items)
                        {
                            table.Cell().Element(CellStyle).Text(item.macn);
                            table.Cell().Element(CellStyle).Text(item.tencn);
                            table.Cell().Element(CellStyle).Text(item.sohoadon.ToString());
                            table.Cell().Element(CellStyle).Text(item.tongdoanhthu.ToString("N0"));
                        }
                    });
                });
            });
        }).GeneratePdf();

        return File(pdf, "application/pdf", "doanh-thu.pdf");
    }

    public async Task<IActionResult> DoctorsExcel(DateTime? fromDate, DateTime? toDate)
    {
        var macn = User.FindFirstValue("MaChiNhanh");
        var items = await BuildDoctorStats(macn, fromDate, toDate);
        if (items.Count == 0)
        {
            return BadRequest("Khong co du lieu.");
        }

        using var workbook = new XLWorkbook();
        var ws = workbook.Worksheets.Add("BacSi");
        ws.Cell(1, 1).Value = "Ma NV";
        ws.Cell(1, 2).Value = "Ho ten";
        ws.Cell(1, 3).Value = "So lan kham";
        ws.Cell(1, 4).Value = "So lan tiem";
        ws.Cell(1, 5).Value = "Doanh thu uoc tinh";

        var row = 2;
        foreach (var item in items)
        {
            ws.Cell(row, 1).Value = item.manv;
            ws.Cell(row, 2).Value = item.hoten;
            ws.Cell(row, 3).Value = item.solankham;
            ws.Cell(row, 4).Value = item.solantiem;
            ws.Cell(row, 5).Value = item.doanhthu;
            row++;
        }

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        stream.Position = 0;
        return File(stream.ToArray(),
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            "bac-si.xlsx");
    }

    public async Task<IActionResult> DoctorsPdf(DateTime? fromDate, DateTime? toDate)
    {
        var macn = User.FindFirstValue("MaChiNhanh");
        var items = await BuildDoctorStats(macn, fromDate, toDate);
        if (items.Count == 0)
        {
            return BadRequest("Khong co du lieu.");
        }

        var pdf = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(30);
                page.Size(PageSizes.A4);
                page.Content().Column(col =>
                {
                    col.Item().Text("Thong ke bac si").FontSize(18).Bold();
                    col.Item().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(2);
                            columns.RelativeColumn(4);
                            columns.RelativeColumn(2);
                            columns.RelativeColumn(2);
                            columns.RelativeColumn(3);
                        });
                        table.Header(header =>
                        {
                            header.Cell().Element(CellStyle).Text("Ma NV");
                            header.Cell().Element(CellStyle).Text("Ho ten");
                            header.Cell().Element(CellStyle).Text("So lan kham");
                            header.Cell().Element(CellStyle).Text("So lan tiem");
                            header.Cell().Element(CellStyle).Text("Doanh thu");
                        });
                        foreach (var item in items)
                        {
                            table.Cell().Element(CellStyle).Text(item.manv);
                            table.Cell().Element(CellStyle).Text(item.hoten);
                            table.Cell().Element(CellStyle).Text(item.solankham.ToString());
                            table.Cell().Element(CellStyle).Text(item.solantiem.ToString());
                            table.Cell().Element(CellStyle).Text(item.doanhthu.ToString("N0"));
                        }
                    });
                });
            });
        }).GeneratePdf();

        return File(pdf, "application/pdf", "bac-si.pdf");
    }

    public async Task<IActionResult> VisitsExcel(DateTime? fromDate, DateTime? toDate, string? period)
    {
        var macn = User.FindFirstValue("MaChiNhanh");
        var normalized = NormalizePeriod(period);
        var items = await BuildVisitStats(macn, fromDate, toDate, normalized);
        if (items.Count == 0)
        {
            return BadRequest("Khong co du lieu.");
        }

        using var workbook = new XLWorkbook();
        var ws = workbook.Worksheets.Add("LuotKham");
        ws.Cell(1, 1).Value = "Ngay";
        ws.Cell(1, 2).Value = "So luot";

        var row = 2;
        foreach (var item in items)
        {
            ws.Cell(row, 1).Value = item.ngay.ToString("dd/MM/yyyy");
            ws.Cell(row, 2).Value = item.soluot;
            row++;
        }

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        stream.Position = 0;
        return File(stream.ToArray(),
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            "luot-kham.xlsx");
    }

    public async Task<IActionResult> VisitsPdf(DateTime? fromDate, DateTime? toDate, string? period)
    {
        var macn = User.FindFirstValue("MaChiNhanh");
        var normalized = NormalizePeriod(period);
        var items = await BuildVisitStats(macn, fromDate, toDate, normalized);
        if (items.Count == 0)
        {
            return BadRequest("Khong co du lieu.");
        }

        var pdf = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(30);
                page.Size(PageSizes.A4);
                page.Content().Column(col =>
                {
                    col.Item().Text("So luot kham").FontSize(18).Bold();
                    if (fromDate.HasValue && toDate.HasValue)
                    {
                        col.Item().Text($"Tu ngay: {fromDate:dd/MM/yyyy} - Den ngay: {toDate:dd/MM/yyyy}");
                    }
                    col.Item().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(3);
                            columns.RelativeColumn(2);
                        });
                        table.Header(header =>
                        {
                            header.Cell().Element(CellStyle).Text("Ngay");
                            header.Cell().Element(CellStyle).Text("So luot");
                        });
                        foreach (var item in items)
                        {
                            table.Cell().Element(CellStyle).Text(item.ngay.ToString("dd/MM/yyyy"));
                            table.Cell().Element(CellStyle).Text(item.soluot.ToString());
                        }
                    });
                });
            });
        }).GeneratePdf();

        return File(pdf, "application/pdf", "luot-kham.pdf");
    }

    public async Task<IActionResult> ProductsExcel(DateTime? fromDate, DateTime? toDate, int top = 10)
    {
        var macn = User.FindFirstValue("MaChiNhanh");
        var items = await BuildProductStats(macn, fromDate, toDate, top);
        if (items.Count == 0)
        {
            return BadRequest("Khong co du lieu.");
        }

        using var workbook = new XLWorkbook();
        var ws = workbook.Worksheets.Add("SanPham");
        ws.Cell(1, 1).Value = "Ma SP";
        ws.Cell(1, 2).Value = "Ten san pham";
        ws.Cell(1, 3).Value = "So luong";
        ws.Cell(1, 4).Value = "Doanh thu";

        var row = 2;
        foreach (var item in items)
        {
            ws.Cell(row, 1).Value = item.masp;
            ws.Cell(row, 2).Value = item.tensp;
            ws.Cell(row, 3).Value = item.tongsoluong;
            ws.Cell(row, 4).Value = item.tongdoanhthu;
            row++;
        }

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        stream.Position = 0;
        return File(stream.ToArray(),
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            "san-pham.xlsx");
    }

    public async Task<IActionResult> ProductsPdf(DateTime? fromDate, DateTime? toDate, int top = 10)
    {
        var macn = User.FindFirstValue("MaChiNhanh");
        var items = await BuildProductStats(macn, fromDate, toDate, top);
        if (items.Count == 0)
        {
            return BadRequest("Khong co du lieu.");
        }

        var pdf = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(30);
                page.Size(PageSizes.A4);
                page.Content().Column(col =>
                {
                    col.Item().Text("Doanh thu san pham").FontSize(18).Bold();
                    col.Item().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(2);
                            columns.RelativeColumn(4);
                            columns.RelativeColumn(2);
                            columns.RelativeColumn(2);
                        });
                        table.Header(header =>
                        {
                            header.Cell().Element(CellStyle).Text("Ma SP");
                            header.Cell().Element(CellStyle).Text("Ten san pham");
                            header.Cell().Element(CellStyle).Text("So luong");
                            header.Cell().Element(CellStyle).Text("Doanh thu");
                        });
                        foreach (var item in items)
                        {
                            table.Cell().Element(CellStyle).Text(item.masp);
                            table.Cell().Element(CellStyle).Text(item.tensp);
                            table.Cell().Element(CellStyle).Text(item.tongsoluong.ToString());
                            table.Cell().Element(CellStyle).Text(item.tongdoanhthu.ToString("N0"));
                        }
                    });
                });
            });
        }).GeneratePdf();

        return File(pdf, "application/pdf", "san-pham.pdf");
    }

    private static IContainer CellStyle(IContainer container)
    {
        return container.Border(1).BorderColor(Colors.Grey.Lighten2).Padding(4);
    }

    private async Task<ManagerRevenueViewModel> BuildRevenueViewModel(DateTime? fromDate, DateTime? toDate, string? macn, string? period)
    {
        var vm = new ManagerRevenueViewModel
        {
            FromDate = fromDate,
            ToDate = toDate,
            BranchId = macn,
            Period = NormalizePeriod(period)
        };

        if (string.IsNullOrWhiteSpace(macn))
        {
            return vm;
        }

        if (!fromDate.HasValue || !toDate.HasValue || fromDate > toDate)
        {
            return vm;
        }

        var branch = await _db.chinhanhs.AsNoTracking()
            .Where(x => x.macn == macn)
            .Select(x => new { x.macn, x.tencn })
            .FirstOrDefaultAsync();

        var from = fromDate.Value.Date;
        var to = toDate.Value.Date.AddDays(1).AddSeconds(-1);
        var paidStatus = "Đã thanh toán";

        var query = _db.hoadons.AsNoTracking()
            .Where(x => x.macn == macn && x.trangthai == paidStatus && x.ngaylap >= from && x.ngaylap <= to);

        var total = await query.SumAsync(x => x.thanhtien ?? (x.tongtien - x.khuyenmai));
        var count = await query.CountAsync();

        vm.BranchName = branch?.tencn;
        vm.BranchRevenue = total;
        vm.Items.Add(new RevenueByBranch
        {
            macn = branch?.macn ?? macn,
            tencn = branch?.tencn ?? macn,
            sohoadon = count,
            tongdoanhthu = total
        });

        // Trend data grouped by period
        var trendRaw = await query
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

        // Revenue sources (exam, vaccination, products)
        vm.Sources = await BuildRevenueSources(macn, from, to);

        return vm;
    }

    private async Task<List<RevenueSourceSlice>> BuildRevenueSources(string? branchId, DateTime from, DateTime to)
    {
        var paidStatus = "Đã thanh toán";

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
            .Where(x => x.hd.trangthai == paidStatus && x.hd.ngaylap >= from && x.hd.ngaylap <= to)
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

    private async Task<List<DoctorStat>> BuildDoctorStats(string? macn, DateTime? fromDate, DateTime? toDate)
    {
        if (string.IsNullOrWhiteSpace(macn))
        {
            return new List<DoctorStat>();
        }

        if (!fromDate.HasValue || !toDate.HasValue || fromDate > toDate)
        {
            return new List<DoctorStat>();
        }

        var previousTimeout = _db.Database.GetCommandTimeout();
        _db.Database.SetCommandTimeout(120);
        try
        {
            var doctors = await _db.nhanviens.AsNoTracking()
                .Where(x => x.macn == macn && x.loainv == "Bác sĩ thú y")
                .Select(x => new { x.manv, x.hoten })
                .ToListAsync();

            var doctorIds = doctors.Select(x => x.manv).ToList();
            if (doctorIds.Count == 0)
            {
                return new List<DoctorStat>();
            }

            var from = fromDate.Value.Date;
            var to = toDate.Value.Date.AddDays(1).AddSeconds(-1);

            var doctorsQuery = _db.nhanviens.AsNoTracking()
                .Where(x => doctorIds.Contains(x.manv));

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

    private async Task<List<ProductStat>> BuildProductStats(string? macn, DateTime? fromDate, DateTime? toDate, int top)
    {
        if (string.IsNullOrWhiteSpace(macn))
        {
            return new List<ProductStat>();
        }

        if (!fromDate.HasValue || !toDate.HasValue || fromDate > toDate)
        {
            return new List<ProductStat>();
        }

        var from = fromDate.Value.Date;
        var to = toDate.Value.Date.AddDays(1).AddSeconds(-1);
        var paidStatus = "Đã thanh toán";

        return await _db.chitietmuasanphams.AsNoTracking()
            .Join(_db.hoadons.AsNoTracking(),
                ct => ct.mahd,
                hd => hd.mahd,
                (ct, hd) => new { ct, hd })
            .Where(x => x.hd.macn == macn && x.hd.trangthai == paidStatus && x.hd.ngaylap >= from && x.hd.ngaylap <= to)
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

    private async Task<List<VisitStat>> BuildVisitStats(string? macn, DateTime? fromDate, DateTime? toDate, string period = "day")
    {
        if (string.IsNullOrWhiteSpace(macn) ||
            !fromDate.HasValue ||
            !toDate.HasValue ||
            fromDate > toDate)
        {
            return new List<VisitStat>();
        }

        var query = _db.chitietkhambenhs.AsNoTracking()
            .Where(x => x.ngaysudung >= fromDate.Value.Date && x.ngaysudung <= toDate.Value.Date.AddDays(1).AddSeconds(-1))
            .Join(_db.nhanviens.AsNoTracking(),
                kb => kb.mabs,
                nv => nv.manv,
                (kb, nv) => new { kb, nv })
            .Where(x => x.nv.macn == macn)
            .Select(x => x.kb);

        var rawData = await query.Select(x => x.ngaysudung).ToListAsync();

        var grouped = rawData
            .GroupBy(date => GetPeriodStart(date, period))
            .Select(g => new VisitStat
            {
                ngay = g.Key,
                soluot = g.Count()
            })
            .OrderBy(x => x.ngay)
            .ToList();

        return grouped;
    }

    private async Task<List<ProductCategorySlice>> BuildProductCategories(string? macn, DateTime? fromDate, DateTime? toDate)
    {
        if (string.IsNullOrWhiteSpace(macn) || !fromDate.HasValue || !toDate.HasValue || fromDate > toDate)
        {
            return new List<ProductCategorySlice>();
        }

        var from = fromDate.Value.Date;
        var to = toDate.Value.Date.AddDays(1).AddSeconds(-1);
        var paidStatus = "Đã thanh toán";

        var categoryRevenue = await _db.chitietmuasanphams.AsNoTracking()
            .Join(_db.hoadons.AsNoTracking(),
                ct => ct.mahd,
                hd => hd.mahd,
                (ct, hd) => new { ct, hd })
            .Where(x => x.hd.macn == macn && x.hd.trangthai == paidStatus && x.hd.ngaylap >= from && x.hd.ngaylap <= to)
            .Join(_db.sanphams.AsNoTracking(),
                x => x.ct.masp,
                sp => sp.masp,
                (x, sp) => new { x.ct, sp })
            .GroupBy(x => x.sp.loaisp)
            .Select(g => new ProductCategorySlice
            {
                Name = g.Key,
                Value = g.Sum(x => x.ct.thanhtien)
            })
            .ToListAsync();

        return categoryRevenue;
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


