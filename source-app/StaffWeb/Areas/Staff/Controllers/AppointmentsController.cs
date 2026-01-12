using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using StaffWeb.Data;
using StaffWeb.Models;
using StaffWeb.Utils;

namespace StaffWeb.Areas.Staff.Controllers;

[Area("Staff")]
[Authorize(Roles = "Staff")]
public class AppointmentsController : Controller
{
    private readonly AppDbContext _db;

    public AppointmentsController(AppDbContext db) => _db = db;

    public async Task<IActionResult> Index()
    {
        var manv = User.FindFirstValue("MaNhanVien");
        var vm = await BuildViewModelAsync(new StaffAppointmentViewModel
        {
            CustomerPhone = Request.Query["CustomerPhone"],
            PetId = Request.Query["PetId"],
            ServiceId = Request.Query["ServiceId"],
            DoctorId = Request.Query["DoctorId"],
            SelectedPendingPackageId = Request.Query["SelectedPendingPackageId"],
            VisitTime = DateTime.TryParse(Request.Query["VisitTime"], out var vt) ? vt : null
        }, manv);
        
        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> Create(StaffAppointmentViewModel vm)
    {
        var manv = User.FindFirstValue("MaNhanVien");

        // Validate required fields
        if (string.IsNullOrWhiteSpace(vm.CustomerPhone))
        {
            ModelState.AddModelError("", "Vui lòng nhập số điện thoại khách hàng.");
            return View("Index", await BuildViewModelAsync(vm, manv));
        }

        if (string.IsNullOrWhiteSpace(vm.PetId))
        {
            ModelState.AddModelError("", "Vui lòng chọn thú cưng.");
            return View("Index", await BuildViewModelAsync(vm, manv));
        }

        if (string.IsNullOrWhiteSpace(vm.ServiceId))
        {
            ModelState.AddModelError("", "Vui lòng chọn dịch vụ.");
            return View("Index", await BuildViewModelAsync(vm, manv));
        }

        if (string.IsNullOrWhiteSpace(vm.DoctorId))
        {
            ModelState.AddModelError("", "Vui lòng chọn bác sĩ.");
            return View("Index", await BuildViewModelAsync(vm, manv));
        }

        if (vm.VisitTime == null)
        {
            ModelState.AddModelError("", "Vui lòng chọn ngày giờ.");
            return View("Index", await BuildViewModelAsync(vm, manv));
        }

        var petExists = await _db.thucungs.AnyAsync(x => x.mathucung == vm.PetId);
        if (!petExists)
        {
            ModelState.AddModelError("", "Mã thú cưng không tồn tại.");
            return View("Index", await BuildViewModelAsync(vm, manv));
        }

        var service = await _db.dichvus.AsNoTracking().FirstOrDefaultAsync(d => d.madv == vm.ServiceId);
        if (service == null)
        {
            ModelState.AddModelError("", "Dịch vụ không tồn tại.");
            return View("Index", await BuildViewModelAsync(vm, manv));
        }

        // Get staff's branch for vaccination package creation
        var macn = await _db.nhanviens
            .AsNoTracking()
            .Where(x => x.manv == manv)
            .Select(x => x.macn)
            .FirstOrDefaultAsync();

        if (string.Equals(service.loai, "Khám bệnh", StringComparison.OrdinalIgnoreCase))
        {
            var conflictCount = await _db.chitietkhambenhs
                .Where(k => k.mabs == vm.DoctorId && k.ngaysudung == vm.VisitTime.Value)
                .CountAsync();
            if (conflictCount >= 3)
            {
                ModelState.AddModelError("", "Khung giờ này đã có 3 lịch khám.");
                return View("Index", await BuildViewModelAsync(vm, manv));
            }

            _db.chitietkhambenhs.Add(new chitietkhambenh
            {
                madv = vm.ServiceId,
                mathucung = vm.PetId,
                ngaysudung = vm.VisitTime.Value,
                mabs = vm.DoctorId,
                trieuchung = vm.Notes,
                chandoan = null,
                matoathuoc = null,
                ngaytaikham = null,
                madanhgia = null,
                ghichu = "Tạo lịch khám trực tiếp tại quầy"
            });
        }
        else if (string.Equals(service.loai, "Tiêm phòng", StringComparison.OrdinalIgnoreCase))
        {
            var vacConflictCount = await _db.chitiettiemphongs
                .Where(k => k.mabs == vm.DoctorId && k.ngaytiem == vm.VisitTime.Value)
                .CountAsync();
            if (vacConflictCount >= 3)
            {
                ModelState.AddModelError("", "Khung giờ này đã có 3 lịch tiêm.");
                return View("Index", await BuildViewModelAsync(vm, manv));
            }

            // If selecting a pending package dose, update that dose's time instead of creating new
            if (!string.IsNullOrWhiteSpace(vm.SelectedPendingPackageId))
            {
                // SelectedPendingPackageId is just the ServiceId (e.g., "806")
                var pkgServiceId = vm.SelectedPendingPackageId;
                
                // Find the next pending dose for this service and pet
                var pendingDose = await _db.chitiettiemphongs
                    .Where(ct => ct.madv == pkgServiceId && 
                                 ct.mathucung == vm.PetId && 
                                 (ct.trangthai == null || EF.Functions.Collate(ct.trangthai, "Latin1_General_CI_AI") == "Chưa tiêm"))
                    .OrderBy(ct => ct.stt)
                    .FirstOrDefaultAsync();

                if (pendingDose == null)
                {
                    ModelState.AddModelError("", "Không tìm thấy liều tiêm đang chờ.");
                    return View("Index", await BuildViewModelAsync(vm, manv));
                }

                // Use raw SQL UPDATE since mabs is part of composite primary key
                await _db.Database.ExecuteSqlRawAsync(
                    "UPDATE chitiettiemphong SET mabs = @mabs, ngaytiem = @ngaytiem " +
                    "WHERE madv = @madv AND mathucung = @mathucung AND mavacxin = @mavacxin AND stt = @stt",
                    new Microsoft.Data.SqlClient.SqlParameter("@mabs", vm.DoctorId),
                    new Microsoft.Data.SqlClient.SqlParameter("@ngaytiem", vm.VisitTime.Value),
                    new Microsoft.Data.SqlClient.SqlParameter("@madv", pendingDose.madv),
                    new Microsoft.Data.SqlClient.SqlParameter("@mathucung", pendingDose.mathucung),
                    new Microsoft.Data.SqlClient.SqlParameter("@mavacxin", pendingDose.mavacxin),
                    new Microsoft.Data.SqlClient.SqlParameter("@stt", pendingDose.stt)
                );
            }
            else
            {
                var isPackage = await _db.tiemgois.AnyAsync(t => t.madv == vm.ServiceId);
                if (isPackage)
                {
                    try
                    {
                        // Call sp_dangky_tiem_goi to create all vaccination doses
                        await _db.Database.ExecuteSqlRawAsync(
                            "EXEC sp_dangky_tiem_goi @madv, @mathucung, @macn, @mabs, @mavacxin",
                            new Microsoft.Data.SqlClient.SqlParameter("@madv", vm.ServiceId),
                            new Microsoft.Data.SqlClient.SqlParameter("@mathucung", vm.PetId),
                            new Microsoft.Data.SqlClient.SqlParameter("@macn", macn ?? "CN001"),
                            new Microsoft.Data.SqlClient.SqlParameter("@mabs", vm.DoctorId),
                            new Microsoft.Data.SqlClient.SqlParameter("@mavacxin", "VX0001")
                        );
                    }
                    catch (Exception ex)
                    {
                        var errorMsg = ex.InnerException?.Message ?? ex.Message;
                        ModelState.AddModelError("", $"Không thể tạo gói tiêm: {errorMsg}");
                        return View("Index", await BuildViewModelAsync(vm, manv));
                    }
                }
                else
                {
                    // Single vaccination - add directly with EF
                    _db.chitiettiemphongs.Add(new chitiettiemphong
                    {
                        stt = 1,
                        madv = vm.ServiceId,
                        mathucung = vm.PetId,
                        mavacxin = "VX0001",
                        mabs = vm.DoctorId,
                        ngaytiem = vm.VisitTime.Value,
                        trangthai = "Chưa tiêm"
                    });
                }
            }
        }

        try
        {
            await _db.SaveChangesAsync();
            TempData["Success"] = "Đã tạo lịch khám thành công.";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            var errorMsg = ex.InnerException?.Message ?? ex.Message;
            ModelState.AddModelError("", $"Không thể tạo lịch khám: {errorMsg}");
            return View("Index", await BuildViewModelAsync(vm, manv));
        }
    }

    private async Task<StaffAppointmentViewModel> BuildViewModelAsync(StaffAppointmentViewModel vm, string? manv)
    {
        // Get staff's branch
        var macn = string.IsNullOrWhiteSpace(manv)
            ? null
            : await _db.nhanviens
                .AsNoTracking()
                .Where(x => x.manv == manv)
                .Select(x => x.macn)
                .FirstOrDefaultAsync();

        // Load customer and their pets if phone is provided
        if (!string.IsNullOrWhiteSpace(vm.CustomerPhone))
        {
            // Get customer info with name from thongtin table
            var connectionString = _db.Database.GetConnectionString();
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                connectionString = _db.Database.GetDbConnection().ConnectionString;
            }

            await using var conn = new Microsoft.Data.SqlClient.SqlConnection(connectionString);
            await conn.OpenAsync();

            await using var cmd = conn.CreateCommand();
            cmd.CommandText =
                "SELECT kh.makh, kh.sdt, tt.hoten " +
                "FROM khachhang kh " +
                "LEFT JOIN thongtin tt ON kh.cccd = tt.cccd " +
                "WHERE kh.sdt = @phone";

            var param = cmd.CreateParameter();
            param.ParameterName = "@phone";
            param.Value = vm.CustomerPhone;
            cmd.Parameters.Add(param);

            await using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                vm.Customer = new CustomerInfo
                {
                    MaKh = reader.IsDBNull(0) ? null : reader.GetString(0),
                    Phone = reader.IsDBNull(1) ? null : reader.GetString(1),
                    Name = reader.IsDBNull(2) ? null : reader.GetString(2)
                };
            }
            
            if (vm.Customer != null)
            {
                vm.Pets = await _db.thucungs
                    .AsNoTracking()
                    .Where(x => x.makh == vm.Customer.MaKh)
                    .OrderBy(x => x.tenthucung)
                    .ToListAsync();
            }
            else
            {
                vm.Pets = new List<thucung>();
            }
        }

        // Load services from staff's branch only
        if (!string.IsNullOrWhiteSpace(macn))
        {
            var services = await _db.chinhanhs
                .AsNoTracking()
                .Where(x => x.macn == macn)
                .SelectMany(x => x.madvs)
                .OrderBy(x => x.tendv)
                .ToListAsync();
            // Include both examination and vaccination services
            vm.Services = services
                .Where(x => TextUtil.Normalize(x.loai).Contains("kham") || TextUtil.Normalize(x.loai).Contains("tiem"))
                .ToList();
        }
        else
        {
            var all = await _db.dichvus
                .AsNoTracking()
                .OrderBy(x => x.tendv)
                .ToListAsync();
            vm.Services = all
                .Where(x => TextUtil.Normalize(x.loai).Contains("kham") || TextUtil.Normalize(x.loai).Contains("tiem"))
                .ToList();
        }

        // Load doctors from staff's branch
        var allStaffQuery = _db.nhanviens.AsNoTracking();
        if (!string.IsNullOrWhiteSpace(macn))
        {
            allStaffQuery = allStaffQuery.Where(x => x.macn == macn);
        }
        var allStaff = await allStaffQuery.OrderBy(x => x.hoten).ToListAsync();

        var doctors = allStaff
            .Where(x => TextUtil.Normalize(x.loainv).Contains("bac si"))
            .ToList();
        
        // Filter out busy doctors if visit time is selected
        if (vm.VisitTime.HasValue)
        {
            var slotStart = new DateTime(
                vm.VisitTime.Value.Year,
                vm.VisitTime.Value.Month,
                vm.VisitTime.Value.Day,
                vm.VisitTime.Value.Hour,
                0,
                0);
            var slotEnd = slotStart.AddHours(1);
            var doctorIds = doctors.Select(x => x.manv).ToList();
            var busyDoctorIds = await _db.chitietkhambenhs
                .AsNoTracking()
                .Where(x => doctorIds.Contains(x.mabs) && x.ngaysudung >= slotStart && x.ngaysudung < slotEnd)
                .GroupBy(x => x.mabs)
                .Select(g => new { DoctorId = g.Key, Count = g.Count() })
                .Where(x => x.Count >= 3)
                .Select(x => x.DoctorId)
                .ToListAsync();
            
            var busyVacDoctorIds = await _db.chitiettiemphongs
                .AsNoTracking()
                .Where(x => doctorIds.Contains(x.mabs) && x.ngaytiem >= slotStart && x.ngaytiem < slotEnd)
                .GroupBy(x => x.mabs)
                .Select(g => new { DoctorId = g.Key, Count = g.Count() })
                .Where(x => x.Count >= 3)
                .Select(x => x.DoctorId)
                .ToListAsync();
            
            var allBusyDoctorIds = busyDoctorIds.Concat(busyVacDoctorIds).Distinct().ToList();
            vm.Doctors = doctors
                .Where(x => !allBusyDoctorIds.Contains(x.manv))
                .ToList();
        }
        else
        {
            vm.Doctors = doctors;
        }

        // Load pending vaccination packages for selected pet (like customer side)
        if (!string.IsNullOrWhiteSpace(vm.PetId))
        {
            var flatRows = await (
                from ct in _db.chitiettiemphongs.AsNoTracking()
                join dv in _db.dichvus.AsNoTracking() on ct.madv equals dv.madv
                where ct.mathucung == vm.PetId
                select new { ct.madv, ServiceName = dv.tendv, ct.stt, ct.trangthai }
            ).ToListAsync();

            vm.PendingPackages = flatRows
                .GroupBy(r => new { r.madv, r.ServiceName })
                .Select(g =>
                {
                    var completed = g.Count(x =>
                        string.Equals(x.trangthai, "Đã tiêm", StringComparison.OrdinalIgnoreCase) ||
                        string.Equals(x.trangthai, "Hoàn thành", StringComparison.OrdinalIgnoreCase));
                    var total = g.Count();
                    var nextDose = g.Where(x =>
                                            !(string.Equals(x.trangthai, "Đã tiêm", StringComparison.OrdinalIgnoreCase) ||
                                              string.Equals(x.trangthai, "Hoàn thành", StringComparison.OrdinalIgnoreCase)))
                                        .OrderBy(x => x.stt)
                                        .Select(x => x.stt)
                                        .FirstOrDefault();
                    return new PendingPackageInfo
                    {
                        ServiceId = g.Key.madv,
                        ServiceName = g.Key.ServiceName,
                        CompletedDoses = completed,
                        PendingDoses = total - completed,
                        NextDoseNumber = nextDose,
                        CompletedCount = completed,
                        TotalCount = total
                    };
                })
                .Where(p => p.PendingDoses > 0)
                .ToList();
        }

        // If a pending package is selected, set ServiceId to match (AFTER loading services and packages)
        if (!string.IsNullOrWhiteSpace(vm.SelectedPendingPackageId))
        {
            if (vm.Services.Any(s => s.madv == vm.SelectedPendingPackageId))
            {
                vm.ServiceId = vm.SelectedPendingPackageId;
            }
        }

        return vm;
    }
}
