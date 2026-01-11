using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StaffWeb.Data;
using StaffWeb.Models;

namespace StaffWeb.Areas.Doctor.Controllers;

[Area("Doctor")]
[Authorize(Roles = "Doctor")]
public class VaccinationController : Controller
{
    private readonly AppDbContext _db;

    public VaccinationController(AppDbContext db) => _db = db;

    // Process vaccination from appointment
    public async Task<IActionResult> ProcessAppointment(string petId, string serviceId, long visitTicks)
    {
        var manv = User.FindFirstValue("MaNhanVien");
        if (string.IsNullOrWhiteSpace(manv))
        {
            return Forbid();
        }

        var visitTime = new DateTime(visitTicks);
        var fromDate = visitTime.Date;
        var toDate = visitTime.Date.AddDays(1).AddTicks(-1);

        // Find the appointment
        var appointment = await _db.chitiettiemphongs
            .Include(x => x.madvNavigation)
                .ThenInclude(x => x.madvNavigation)
            .Include(x => x.mavacxinNavigation)
            .Include(x => x.mathucungNavigation)
            .FirstOrDefaultAsync(x =>
                x.mathucung == petId &&
                x.madv == serviceId &&
                x.mabs == manv &&
                x.ngaytiem >= fromDate &&
                x.ngaytiem <= toDate);

        if (appointment == null)
        {
            TempData["Error"] = "Không tìm thấy lịch tiêm.";
            return RedirectToAction("Index", "Appointments");
        }

        // Load all vaccines for dropdown
        var vaccines = await _db.vacxins
            .AsNoTracking()
            .OrderBy(x => x.tenvacxin)
            .Select(x => new { x.mavacxin, x.tenvacxin })
            .ToListAsync();

        var vm = new ProcessVaccinationViewModel
        {
            Stt = appointment.stt,
            PetId = appointment.mathucung,
            PetName = appointment.mathucungNavigation.tenthucung,
            ServiceId = appointment.madv,
            ServiceName = appointment.madvNavigation?.madvNavigation?.tendv ?? appointment.madv,
            VisitDate = appointment.ngaytiem,
            Status = appointment.trangthai ?? "Chưa tiêm",
            AvailableVaccines = vaccines.Select(v => new VaccineOption 
            { 
                MaVacxin = v.mavacxin, 
                TenVacxin = v.tenvacxin 
            }).ToList()
        };

        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> ProcessAppointment(ProcessVaccinationViewModel vm)
    {
        var manv = User.FindFirstValue("MaNhanVien");
        if (string.IsNullOrWhiteSpace(manv))
        {
            return Forbid();
        }

        try
        {
            if (string.IsNullOrWhiteSpace(vm.SelectedVaccineId))
            {
                TempData["Error"] = "Vui lòng chọn vaccine.";
                return RedirectToAction("Index", "Appointments");
            }

            // Get the current record
            var record = await _db.chitiettiemphongs
                .FirstOrDefaultAsync(x =>
                    x.stt == vm.Stt &&
                    x.mathucung == vm.PetId &&
                    x.madv == vm.ServiceId);

            if (record == null)
            {
                TempData["Error"] = "Không tìm thấy bản ghi.";
                return RedirectToAction("Index", "Appointments");
            }

            // Always delete and recreate with selected vaccine (composite key)
            var oldMaDv = record.madv;
            var oldMaThucung = record.mathucung;
            var oldNgayTiem = record.ngaytiem;

            _db.chitiettiemphongs.Remove(record);
            await _db.SaveChangesAsync();

            var newRecord = new chitiettiemphong
            {
                stt = vm.Stt,
                madv = oldMaDv,
                mathucung = oldMaThucung,
                mavacxin = vm.SelectedVaccineId,
                mabs = manv,
                ngaytiem = oldNgayTiem,
                trangthai = "Đã tiêm"
            };

            _db.chitiettiemphongs.Add(newRecord);
            await _db.SaveChangesAsync();

            // Check if all doses in package are completed
            var allDosesInPackage = await _db.chitiettiemphongs
                .Where(x =>
                    x.mathucung == vm.PetId &&
                    x.madv == vm.ServiceId)
                .ToListAsync();

            var allCompleted = allDosesInPackage.All(x => x.trangthai == "Đã tiêm");

            if (allCompleted)
            {
                // Mark all as "Hoàn thành"
                foreach (var dose in allDosesInPackage)
                {
                    dose.trangthai = "Hoàn thành";
                }
                await _db.SaveChangesAsync();
            }

            TempData["Success"] = "Đã xác nhận tiêm thành công.";
            return RedirectToAction("Index", "Appointments");
        }
        catch (Exception ex)
        {
            TempData["Error"] = "Có lỗi xảy ra: " + ex.Message;
            return RedirectToAction("Index", "Appointments");
        }
    }

    public async Task<IActionResult> Create()
    {
        var manv = User.FindFirstValue("MaNhanVien");
        var vm = await BuildViewModel(new VaccinationRecordViewModel(), manv);
        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> Create(VaccinationRecordViewModel vm, string? action)
    {
        var manv = User.FindFirstValue("MaNhanVien");
        if (string.IsNullOrWhiteSpace(manv))
        {
            return Forbid();
        }

        vm.PetId = vm.PetId?.Trim();
        vm.SelectedPendingServiceId = vm.SelectedPendingServiceId?.Trim();

        if (string.Equals(action, "load", StringComparison.OrdinalIgnoreCase))
        {
            return View(await BuildViewModel(vm, manv));
        }

        if (string.IsNullOrWhiteSpace(vm.PetId))
        {
            ModelState.AddModelError("PetId", "Vui long nhap ma thu cung.");
            return View(await BuildViewModel(vm, manv));
        }

        try
        {
            var macn = await _db.nhanviens
                .AsNoTracking()
                .Where(x => x.manv == manv)
                .Select(x => x.macn)
                .FirstOrDefaultAsync();

            // Only handle updating pending packages
            if (vm.SelectedPendingStt.HasValue &&
                !string.IsNullOrWhiteSpace(vm.SelectedPendingServiceId))
            {
                var pending = await _db.chitiettiemphongs
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x =>
                        x.mathucung == vm.PetId &&
                        x.madv == vm.SelectedPendingServiceId &&
                        x.stt == vm.SelectedPendingStt.Value &&
                        (x.trangthai == null || x.trangthai == "Cho tiem" || x.trangthai == "Dang tiem"));
                if (pending == null)
                {
                    ModelState.AddModelError("", "Khong tim thay dot tiem dang cho.");
                    return View(await BuildViewModel(vm, manv));
                }

                // Use sp_xacnhan_da_tiem stored procedure
                await _db.Database.ExecuteSqlRawAsync(
                    "EXEC sp_xacnhan_da_tiem @madv, @mathucung, @stt, @mabs",
                    new Microsoft.Data.SqlClient.SqlParameter("@madv", vm.SelectedPendingServiceId),
                    new Microsoft.Data.SqlClient.SqlParameter("@mathucung", vm.PetId),
                    new Microsoft.Data.SqlClient.SqlParameter("@stt", vm.SelectedPendingStt.Value),
                    new Microsoft.Data.SqlClient.SqlParameter("@mabs", manv)
                );
                TempData["Success"] = "Da ghi nhan dot tiem trong goi.";
                return RedirectToAction(nameof(Create));
            }

            ModelState.AddModelError("", "Vui long chon dot tiem can cap nhat.");
            return View(await BuildViewModel(vm, manv));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", "Khong the cap nhat. Vui long thu lai.");
            ModelState.AddModelError("", ex.Message);
            return View(await BuildViewModel(vm, manv));
        }
    }

    private async Task<VaccinationRecordViewModel> BuildViewModel(VaccinationRecordViewModel vm, string? manv)
    {
        // Load only pending packages - no new vaccination creation options
        if (!string.IsNullOrWhiteSpace(vm.PetId))
        {
            vm.PendingPackages = await (
                from ct in _db.chitiettiemphongs.AsNoTracking()
                join dv in _db.dichvus.AsNoTracking() on ct.madv equals dv.madv into dvj
                from dv in dvj.DefaultIfEmpty()
                join vx in _db.vacxins.AsNoTracking() on ct.mavacxin equals vx.mavacxin into vxj
                from vx in vxj.DefaultIfEmpty()
                where ct.mathucung == vm.PetId &&
                      (ct.trangthai == null || ct.trangthai == "Cho tiem" || ct.trangthai == "Dang tiem")
                orderby ct.ngaytiem
                select new VaccinationPendingItem
                {
                    Stt = ct.stt,
                    ServiceId = ct.madv,
                    ServiceName = dv != null ? dv.tendv : ct.madv,
                    VaccineId = ct.mavacxin,
                    VaccineName = vx != null ? vx.tenvacxin : ct.mavacxin,
                    PlannedDate = ct.ngaytiem,
                    Status = ct.trangthai
                }).ToListAsync();
        }

        return vm;
    }
}
