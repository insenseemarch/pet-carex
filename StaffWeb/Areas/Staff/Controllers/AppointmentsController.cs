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
        var vm = await BuildViewModelAsync(new StaffAppointmentViewModel(), manv);
        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> Index(StaffAppointmentViewModel vm, string? action)
    {
        var manv = User.FindFirstValue("MaNhanVien");
        if (string.Equals(action, "load", StringComparison.OrdinalIgnoreCase))
        {
            return View(await BuildViewModelAsync(vm, manv));
        }

        if (string.IsNullOrWhiteSpace(vm.PetId) ||
            string.IsNullOrWhiteSpace(vm.ServiceId) ||
            string.IsNullOrWhiteSpace(vm.DoctorId) ||
            vm.VisitTime == null)
        {
            ModelState.AddModelError("", "Vui long nhap day du thong tin bat buoc.");
            return View(await BuildViewModelAsync(vm, manv));
        }

        var petExists = await _db.thucungs.AnyAsync(x => x.mathucung == vm.PetId);
        if (!petExists)
        {
            ModelState.AddModelError("", "Ma thu cung khong ton tai.");
            return View(await BuildViewModelAsync(vm, manv));
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
            ghichu = "Tao lich kham truc tiep"
        });

        try
        {
            await _db.SaveChangesAsync();
            TempData["Success"] = "Da tao lich kham.";
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            ModelState.AddModelError("", "Khong the tao lich kham. Vui long thu lai.");
            return View(await BuildViewModelAsync(vm, manv));
        }
    }

    private async Task<StaffAppointmentViewModel> BuildViewModelAsync(StaffAppointmentViewModel vm, string? manv)
    {
        var macn = string.IsNullOrWhiteSpace(manv)
            ? null
            : await _db.nhanviens
                .AsNoTracking()
                .Where(x => x.manv == manv)
                .Select(x => x.macn)
                .FirstOrDefaultAsync();

        if (!string.IsNullOrWhiteSpace(macn))
        {
            var services = await _db.chinhanhs
                .AsNoTracking()
                .Where(x => x.macn == macn)
                .SelectMany(x => x.madvs)
                .OrderBy(x => x.tendv)
                .ToListAsync();
            vm.Services = services
                .Where(x => TextUtil.Normalize(x.loai).Contains("kham"))
                .ToList();
        }
        else
        {
            var all = await _db.dichvus
                .AsNoTracking()
                .OrderBy(x => x.tendv)
                .ToListAsync();
            vm.Services = all
                .Where(x => TextUtil.Normalize(x.loai).Contains("kham"))
                .ToList();
        }

        var allStaffQuery = _db.nhanviens.AsNoTracking();
        if (!string.IsNullOrWhiteSpace(macn))
        {
            allStaffQuery = allStaffQuery.Where(x => x.macn == macn);
        }
        var allStaff = await allStaffQuery.OrderBy(x => x.hoten).ToListAsync();

        var doctors = allStaff
            .Where(x => TextUtil.Normalize(x.loainv).Contains("bac si"))
            .ToList();
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
            vm.Doctors = doctors
                .Where(x => !busyDoctorIds.Contains(x.manv))
                .ToList();
        }
        else
        {
            vm.Doctors = doctors;
        }

        return vm;
    }
}
