using KhachHangWeb.Data;
using KhachHangWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KhachHangWeb.Controllers;

[Authorize]
public class BookingController : Controller
{
    private readonly AppDbContext _db;
    public BookingController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<IActionResult> Index(string? branchId, string? doctorId, string? serviceId, string? petId, string? slot, DateTime? date)
    {
        var makh = User.FindFirst("MaKhachHang")?.Value;
        if (string.IsNullOrWhiteSpace(makh)) return RedirectToAction("Login", "Account");

        var vm = await BuildBookingViewModel(makh, branchId, doctorId, serviceId, petId, slot, date);
        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> Create(BookingViewModel vm)
    {
        var makh = User.FindFirst("MaKhachHang")?.Value;
        if (string.IsNullOrWhiteSpace(makh)) return RedirectToAction("Login", "Account");

        var filled = await BuildBookingViewModel(makh, vm.BranchId, vm.DoctorId, vm.ServiceId, vm.PetId, vm.Slot, vm.Date);
        filled.PetId = vm.PetId;
        filled.Slot = vm.Slot;
        filled.Note = vm.Note;

        if (string.IsNullOrWhiteSpace(filled.BranchId))
            ModelState.AddModelError("", "Vui lòng chọn chi nhánh.");

        if (string.IsNullOrWhiteSpace(filled.PetId))
            ModelState.AddModelError("", "Vui lòng chọn thú cưng.");

        if (string.IsNullOrWhiteSpace(filled.ServiceId))
            ModelState.AddModelError("", "Vui lòng chọn dịch vụ.");

        if (string.IsNullOrWhiteSpace(filled.DoctorId))
            ModelState.AddModelError("", "Vui lòng chọn bác sĩ.");

        if (string.IsNullOrWhiteSpace(filled.Slot))
            ModelState.AddModelError("", "Vui lòng chọn khung giờ.");

        if (!ModelState.IsValid) return View("Index", filled);

        var time = TimeSpan.Parse(filled.Slot);
        var bookingTime = filled.Date.Date.Add(time);
        var serviceOk = await _db.chinhanhs
            .Where(c => c.macn == filled.BranchId)
            .SelectMany(c => c.madvs)
            .AnyAsync(d => d.madv == filled.ServiceId && EF.Functions.Collate(d.loai, "Latin1_General_CI_AI") == "Khám bệnh");

        if (!serviceOk)
        {
            ModelState.AddModelError("", "D?ch v? kh?ng thu?c chi nh?nh ?? ch?n.");
            return View("Index", filled);
        }

        var petOk = await _db.thucungs.AnyAsync(t => t.mathucung == filled.PetId && t.makh == makh);
        if (!petOk)
        {
            ModelState.AddModelError("", "Thú cưng không hợp lệ.");
            return View("Index", filled);
        }

        var conflictCount = await _db.chitietkhambenhs
            .Where(k => k.mabs == filled.DoctorId && k.ngaysudung == bookingTime)
            .CountAsync();
        if (conflictCount >= 3)
        {
            ModelState.AddModelError("", "Khung giờ này đã có lịch khám.");
            return View("Index", filled);
        }

        _db.chitietkhambenhs.Add(new chitietkhambenh
        {
            madv = filled.ServiceId!,
            mathucung = filled.PetId!,
            ngaysudung = bookingTime,
            mabs = filled.DoctorId!,
            trieuchung = filled.Note,
            chandoan = null,
            matoathuoc = null,
            ngaytaikham = null,
            madanhgia = null,
            ghichu = null
        });

        await _db.SaveChangesAsync();
        TempData["Booked"] = true;

        return RedirectToAction(nameof(Index), new
        {
            branchId = filled.BranchId,
            doctorId = filled.DoctorId,
            serviceId = filled.ServiceId,
            date = filled.Date.ToString("yyyy-MM-dd")
        });
    }

    [HttpGet]
    public async Task<IActionResult> Schedule(string? doctorId, DateTime? date)
    {
        var vm = new DoctorScheduleViewModel
        {
            Date = (date ?? DateTime.Today).Date
        };

        vm.Doctors = await _db.nhanviens
            .Where(n => EF.Functions.Collate(n.loainv, "Latin1_General_CI_AI") == "Bac si thu y")
            .OrderBy(n => n.hoten)
            .Select(n => new SelectOption { Id = n.manv, Name = n.hoten })
            .ToListAsync();

        vm.DoctorId = doctorId ?? vm.Doctors.FirstOrDefault()?.Id;
        if (string.IsNullOrWhiteSpace(vm.DoctorId)) return View(vm);

        var start = vm.Date.Date;
        var end = start.AddDays(1);

        vm.Items = await _db.chitietkhambenhs
            .Where(k => k.mabs == vm.DoctorId && k.ngaysudung >= start && k.ngaysudung < end)
            .OrderBy(k => k.ngaysudung)
            .Select(k => new DoctorScheduleItem
            {
                Time = k.ngaysudung,
                PetId = k.mathucung,
                PetName = k.mathucungNavigation.tenthucung,
                BranchName = k.mabsNavigation.macnNavigation.tencn,
                ServiceName = k.madvNavigation.tendv
            })
            .ToListAsync();

        return View(vm);
    }

    private async Task<BookingViewModel> BuildBookingViewModel(
        string makh,
        string? branchId,
        string? doctorId,
        string? serviceId,
        string? petId,
        string? slotId,
        DateTime? date)
    {
        var vm = new BookingViewModel
        {
            Date = (date ?? DateTime.Today).Date,
            Slots = BuildSlots()
        };

        vm.Branches = await _db.chinhanhs
            .OrderBy(c => c.tencn)
            .Select(c => new SelectOption { Id = c.macn, Name = c.tencn })
            .ToListAsync();

        vm.BranchId = string.IsNullOrWhiteSpace(branchId)
            ? vm.Branches.FirstOrDefault()?.Id
            : branchId;

        vm.Pets = await _db.thucungs
            .Where(t => t.makh == makh)
            .OrderBy(t => t.tenthucung)
            .Select(t => new SelectOption { Id = t.mathucung, Name = t.tenthucung })
            .ToListAsync();

        vm.PetId = vm.Pets.FirstOrDefault()?.Id;
        if (!string.IsNullOrWhiteSpace(petId) && vm.Pets.Any(p => p.Id == petId))
            vm.PetId = petId;

        vm.Services = new List<SelectOption>();
        if (!string.IsNullOrWhiteSpace(vm.BranchId))
        {
            vm.Services = await _db.chinhanhs
                .Where(c => c.macn == vm.BranchId)
                .SelectMany(c => c.madvs)
                .Where(d => EF.Functions.Collate(d.loai, "Latin1_General_CI_AI") == "Khám bệnh")
                .OrderBy(d => d.tendv)
                .Select(d => new SelectOption { Id = d.madv, Name = d.tendv })
                .ToListAsync();
        }

        vm.ServiceId = string.IsNullOrWhiteSpace(serviceId)
            ? vm.Services.FirstOrDefault()?.Id
            : vm.Services.Any(s => s.Id == serviceId) ? serviceId : vm.Services.FirstOrDefault()?.Id;

        var requestedDate = vm.Date.Date;
        var dayStart = requestedDate;
        var dayEnd = requestedDate.AddDays(1);

        vm.Doctors = await _db.nhanviens
            .Where(n => n.macn == vm.BranchId
                && EF.Functions.Collate(n.loainv, "Latin1_General_CI_AI") == "Bac si thu y")
            .OrderBy(n => n.hoten)
            .Select(n => new SelectOption { Id = n.manv, Name = n.hoten })
            .ToListAsync();

        if (!string.IsNullOrWhiteSpace(doctorId) && vm.Doctors.Any(d => d.Id == doctorId))
            vm.DoctorId = doctorId;
        else
            vm.DoctorId = null;

        vm.Slot = null;
        if (!string.IsNullOrWhiteSpace(slotId) && vm.Slots.Any(s => s.Id == slotId))
            vm.Slot = slotId;

        if (!string.IsNullOrWhiteSpace(vm.Slot))
        {
            var slotTime = TimeSpan.Parse(vm.Slot);
            var slotStart = requestedDate.Add(slotTime);
            var slotEnd = slotStart.AddHours(1);

            if (string.IsNullOrWhiteSpace(vm.DoctorId))
            {
                var doctorIds = vm.Doctors.Select(d => d.Id).ToList();
                var fullDoctorIds = await _db.chitietkhambenhs
                    .Where(k => doctorIds.Contains(k.mabs) && k.ngaysudung >= slotStart && k.ngaysudung < slotEnd)
                    .GroupBy(k => k.mabs)
                    .Select(g => new { DoctorId = g.Key, Count = g.Count() })
                    .Where(x => x.Count >= 3)
                    .Select(x => x.DoctorId)
                    .ToListAsync();

                vm.Doctors = vm.Doctors.Where(d => !fullDoctorIds.Contains(d.Id)).ToList();
            }
            else
            {
                var slotCount = await _db.chitietkhambenhs
                    .Where(k => k.mabs == vm.DoctorId && k.ngaysudung >= slotStart && k.ngaysudung < slotEnd)
                    .CountAsync();
                if (slotCount >= 3)
                    vm.Slot = null;
            }
        }

        if (!string.IsNullOrWhiteSpace(vm.DoctorId))
        {
            var booked = await _db.chitietkhambenhs
                .Where(k => k.mabs == vm.DoctorId && k.ngaysudung >= dayStart && k.ngaysudung < dayEnd)
                .Select(k => k.ngaysudung)
                .ToListAsync();

            var slotCounts = new Dictionary<string, int>();
            foreach (var slot in vm.Slots)
            {
                var slotTime = TimeSpan.Parse(slot.Id);
                var slotStart = dayStart.Add(slotTime);
                var slotEnd = slotStart.AddHours(1);
                var count = booked.Count(t => t >= slotStart && t < slotEnd);
                if (count > 0) slotCounts[slot.Id] = count;
            }

            vm.Slots = vm.Slots.Where(s => !slotCounts.ContainsKey(s.Id) || slotCounts[s.Id] < 3).ToList();
        }

        if (!string.IsNullOrWhiteSpace(vm.Slot) && vm.Slots.All(s => s.Id != vm.Slot))
            vm.Slot = null;

        vm.DoctorName = vm.Doctors.FirstOrDefault(d => d.Id == vm.DoctorId)?.Name;

        return vm;
    }

    private static List<SelectOption> BuildSlots() => new()
    {
        new SelectOption { Id = "08:00", Name = "08:00 - 09:00" },
        new SelectOption { Id = "09:00", Name = "09:00 - 10:00" },
        new SelectOption { Id = "10:00", Name = "10:00 - 11:00" },
        new SelectOption { Id = "13:00", Name = "13:00 - 14:00" },
        new SelectOption { Id = "14:00", Name = "14:00 - 15:00" },
        new SelectOption { Id = "15:00", Name = "15:00 - 16:00" },
        new SelectOption { Id = "16:00", Name = "16:00 - 17:00" }
    };
}
