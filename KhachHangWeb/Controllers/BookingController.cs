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
    public async Task<IActionResult> Index(string? branchId, string? doctorId, DateTime? date)
    {
        var makh = User.FindFirst("MaKhachHang")?.Value;
        if (string.IsNullOrWhiteSpace(makh)) return RedirectToAction("Login", "Account");

        var vm = await BuildBookingViewModel(makh, branchId, doctorId, date);
        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> Create(BookingViewModel vm)
    {
        var makh = User.FindFirst("MaKhachHang")?.Value;
        if (string.IsNullOrWhiteSpace(makh)) return RedirectToAction("Login", "Account");

        var filled = await BuildBookingViewModel(makh, vm.BranchId, vm.DoctorId, vm.Date);
        filled.PetId = vm.PetId;
        filled.Slot = vm.Slot;
        filled.Note = vm.Note;

        if (string.IsNullOrWhiteSpace(filled.BranchId))
            ModelState.AddModelError("", "Vui lòng chọn chi nhánh.");

        if (string.IsNullOrWhiteSpace(filled.PetId))
            ModelState.AddModelError("", "Vui lòng chọn thú cưng.");

        if (string.IsNullOrWhiteSpace(filled.DoctorId))
            ModelState.AddModelError("", "Vui lòng chọn bác sĩ.");

        if (string.IsNullOrWhiteSpace(filled.Slot))
            ModelState.AddModelError("", "Vui lòng chọn khung giờ.");

        if (!ModelState.IsValid) return View("Index", filled);

        var time = TimeSpan.Parse(filled.Slot);
        var bookingTime = filled.Date.Date.Add(time);

        var madv = await _db.dichvus
            .Where(d => d.loai == "Khám bệnh")
            .OrderBy(d => d.madv)
            .Select(d => d.madv)
            .FirstOrDefaultAsync();

        if (string.IsNullOrWhiteSpace(madv))
        {
            ModelState.AddModelError("", "Không tìm thấy dịch vụ khám bệnh.");
            return View("Index", filled);
        }

        var petOk = await _db.thucungs.AnyAsync(t => t.mathucung == filled.PetId && t.makh == makh);
        if (!petOk)
        {
            ModelState.AddModelError("", "Thú cưng không hợp lệ.");
            return View("Index", filled);
        }

        var conflict = await _db.chitietkhambenhs.AnyAsync(k =>
            k.mabs == filled.DoctorId && k.ngaysudung == bookingTime);
        if (conflict)
        {
            ModelState.AddModelError("", "Khung giờ này đã có lịch khám.");
            return View("Index", filled);
        }

        _db.chitietkhambenhs.Add(new chitietkhambenh
        {
            madv = madv,
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
            .Where(n => n.loainv == "Bác sĩ thú y")
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

        vm.Doctors = await _db.nhanviens
            .Where(n => n.macn == vm.BranchId && n.loainv == "Bác sĩ thú y")
            .OrderBy(n => n.hoten)
            .Select(n => new SelectOption { Id = n.manv, Name = n.hoten })
            .ToListAsync();

        vm.DoctorId = string.IsNullOrWhiteSpace(doctorId)
            ? vm.Doctors.FirstOrDefault()?.Id
            : doctorId;

        vm.DoctorName = vm.Doctors.FirstOrDefault(d => d.Id == vm.DoctorId)?.Name;
        vm.Slot = vm.Slots.FirstOrDefault()?.Id;

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
