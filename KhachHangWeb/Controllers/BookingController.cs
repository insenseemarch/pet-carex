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
    public async Task<IActionResult> Index(string? branchId, string? doctorId, string? serviceId, string? petId, string? slot, DateTime? date, string? selectedPendingPackageId)
    {
        var makh = User.FindFirst("MaKhachHang")?.Value;
        if (string.IsNullOrWhiteSpace(makh)) return RedirectToAction("Login", "Account");

        var vm = await BuildBookingViewModel(makh, branchId, doctorId, serviceId, petId, slot, date);
        vm.SelectedPendingPackageId = selectedPendingPackageId;
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
        filled.SelectedPendingPackageId = vm.SelectedPendingPackageId;

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

        var time = TimeSpan.Parse(filled.Slot!);
        var bookingTime = filled.Date.Date.Add(time);
        var service = await _db.dichvus.AsNoTracking().FirstOrDefaultAsync(d => d.madv == filled.ServiceId);

        if (service == null)
        {
            ModelState.AddModelError("", "Dịch vụ không tồn tại hoặc không thuộc chi nhánh.");
            return View("Index", filled);
        }

        var petOk = await _db.thucungs.AnyAsync(t => t.mathucung == filled.PetId && t.makh == makh);
        if (!petOk)
        {
            ModelState.AddModelError("", "Thú cưng không hợp lệ.");
            return View("Index", filled);
        }

        if (string.Equals(service.loai, "Khám bệnh", StringComparison.OrdinalIgnoreCase))
        {
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
        }
        else if (string.Equals(service.loai, "Tiêm phòng", StringComparison.OrdinalIgnoreCase))
        {
            var vacConflictCount = await _db.chitiettiemphongs
                .Where(k => k.mabs == filled.DoctorId && k.ngaytiem == bookingTime)
                .CountAsync();
            if (vacConflictCount >= 3)
            {
                ModelState.AddModelError("", "Khung giờ này đã có lịch tiêm.");
                return View("Index", filled);
            }

            // Check if user selected a pending package dose to continue
            if (!string.IsNullOrWhiteSpace(filled.SelectedPendingPackageId))
            {
                var parts = filled.SelectedPendingPackageId.Split('|');
                chitiettiemphong? pendingDose = null;
                if (parts.Length == 3)
                {
                    // ServiceId|VaccineId|DoseNumber (older format)
                    var pkgServiceId = parts[0];
                    var pkgVaccineId = parts[1];
                    if (!int.TryParse(parts[2], out var pkgDoseNumber)) pkgDoseNumber = 0;

                    pendingDose = await _db.chitiettiemphongs.FirstOrDefaultAsync(ct =>
                        ct.madv == pkgServiceId && ct.mathucung == filled.PetId &&
                        ct.mavacxin == pkgVaccineId && ct.stt == pkgDoseNumber &&
                        (ct.trangthai == null || EF.Functions.Collate(ct.trangthai, "Latin1_General_CI_AI") == "Chưa tiêm"));
                }
                else if (parts.Length >= 1 && !string.IsNullOrWhiteSpace(parts[0]))
                {
                    // ServiceId only (aggregate package) → pick the earliest pending dose across vaccines
                    var pkgServiceId = parts[0];
                    pendingDose = await _db.chitiettiemphongs
                        .Where(ct => ct.madv == pkgServiceId && ct.mathucung == filled.PetId && (ct.trangthai == null || EF.Functions.Collate(ct.trangthai, "Latin1_General_CI_AI") == "Chưa tiêm"))
                        .OrderBy(ct => ct.stt)
                        .FirstOrDefaultAsync();
                }

                if (pendingDose == null)
                {
                    ModelState.AddModelError("", "Không tìm thấy liều tiêm đang chờ.");
                    return View("Index", filled);
                }

                // Update time for the selected pending dose
                pendingDose.ngaytiem = bookingTime;
                _db.chitiettiemphongs.Update(pendingDose);
            }
            else
            {
                // No pending package selected - create new booking
                var isPackage = await _db.tiemgois.AnyAsync(t => t.madv == filled.ServiceId);
                if (isPackage)
                {
                    try
                    {
                        // Call sp_dangky_tiem_goi to create all vaccination doses
                        await _db.Database.ExecuteSqlRawAsync(
                            "EXEC sp_dangky_tiem_goi @madv, @mathucung, @macn, @mabs, @mavacxin",
                            new Microsoft.Data.SqlClient.SqlParameter("@madv", filled.ServiceId),
                            new Microsoft.Data.SqlClient.SqlParameter("@mathucung", filled.PetId),
                            new Microsoft.Data.SqlClient.SqlParameter("@macn", filled.BranchId),
                            new Microsoft.Data.SqlClient.SqlParameter("@mabs", filled.DoctorId),
                            new Microsoft.Data.SqlClient.SqlParameter("@mavacxin", "VX0001")
                        );
                    }
                    catch (Exception ex)
                    {
                        var errorMsg = ex.InnerException?.Message ?? ex.Message;
                        ModelState.AddModelError("", $"Không thể tạo gói tiêm: {errorMsg}");
                        return View("Index", filled);
                    }
                }
                else
                {
                    // Single vaccination - add directly with EF
                    _db.chitiettiemphongs.Add(new chitiettiemphong
                    {
                        stt = 1,
                        madv = filled.ServiceId!,
                        mathucung = filled.PetId!,
                        mavacxin = "VX0001",
                        mabs = filled.DoctorId!,
                        ngaytiem = bookingTime,
                        trangthai = "Chưa tiêm"
                    });
                }
            }
        }

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
    public async Task<IActionResult> Schedule(string? branchId, string? doctorId, DateTime? date)
    {
        var vm = new DoctorScheduleViewModel
        {
            Date = (date ?? DateTime.Today).Date
        };

        vm.Branches = await _db.chinhanhs
            .OrderBy(c => c.tencn)
            .Select(c => new SelectOption { Id = c.macn, Name = c.tencn })
            .ToListAsync();

        vm.BranchId = string.IsNullOrWhiteSpace(branchId)
            ? vm.Branches.FirstOrDefault()?.Id
            : branchId;

        vm.Doctors = await _db.nhanviens
            .Where(n => n.macn == vm.BranchId && EF.Functions.Collate(n.loainv, "Latin1_General_CI_AI") == "Bac si thu y")
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
            // show both khám bệnh and tiêm phòng services
            vm.Services = await _db.chinhanhs
                .Where(c => c.macn == vm.BranchId)
                .SelectMany(c => c.madvs)
                .Where(d => EF.Functions.Collate(d.loai, "Latin1_General_CI_AI") == "Khám bệnh" || EF.Functions.Collate(d.loai, "Latin1_General_CI_AI") == "Tiêm phòng")
                .OrderBy(d => d.tendv)
                .Select(d => new SelectOption { Id = d.madv, Name = d.tendv })
                .ToListAsync();
        }

        vm.ServiceId = string.IsNullOrWhiteSpace(serviceId)
            ? vm.Services.FirstOrDefault()?.Id
            : vm.Services.Any(s => s.Id == serviceId) ? serviceId : vm.Services.FirstOrDefault()?.Id;

        // set selected service information (type & package)
        if (!string.IsNullOrWhiteSpace(vm.ServiceId))
        {
            var svc = await _db.dichvus.AsNoTracking().FirstOrDefaultAsync(x => x.madv == vm.ServiceId);
            vm.ServiceType = svc?.loai;
            vm.SelectedServiceIsPackage = svc != null && await _db.tiemgois.AnyAsync(t => t.madv == vm.ServiceId);
            
            // Load package discount if applicable
            if (vm.SelectedServiceIsPackage && svc != null)
            {
                var packageInfo = await _db.tiemgois.AsNoTracking().FirstOrDefaultAsync(t => t.madv == vm.ServiceId);
                if (packageInfo != null)
                {
                    vm.PackageDiscountPercent = packageInfo.phantramgiamgia;
                    vm.OriginalPrice = svc.gia;
                }
            }
        }

        // Load pending vaccination packages for the selected pet (aggregate by package)
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
                        VaccineId = string.Empty,
                        VaccineName = string.Empty,
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
                var examCounts = await _db.chitietkhambenhs
                    .Where(k => doctorIds.Contains(k.mabs) && k.ngaysudung >= slotStart && k.ngaysudung < slotEnd)
                    .GroupBy(k => k.mabs)
                    .Select(g => new { DoctorId = g.Key, Count = g.Count() })
                    .ToListAsync();
                var vacCounts = await _db.chitiettiemphongs
                    .Where(k => doctorIds.Contains(k.mabs) && k.ngaytiem >= slotStart && k.ngaytiem < slotEnd)
                    .GroupBy(k => k.mabs)
                    .Select(g => new { DoctorId = g.Key, Count = g.Count() })
                    .ToListAsync();
                var totalCounts = examCounts.Concat(vacCounts)
                    .GroupBy(x => x.DoctorId)
                    .Select(g => new { DoctorId = g.Key, Total = g.Sum(x => x.Count) })
                    .Where(x => x.Total >= 3)
                    .Select(x => x.DoctorId)
                    .ToList();

                vm.Doctors = vm.Doctors.Where(d => !totalCounts.Contains(d.Id)).ToList();
            }
            else
            {
                var examCount = await _db.chitietkhambenhs
                    .Where(k => k.mabs == vm.DoctorId && k.ngaysudung >= slotStart && k.ngaysudung < slotEnd)
                    .CountAsync();
                var vacCount = await _db.chitiettiemphongs
                    .Where(k => k.mabs == vm.DoctorId && k.ngaytiem >= slotStart && k.ngaytiem < slotEnd)
                    .CountAsync();
                if (examCount + vacCount >= 3)
                    vm.Slot = null;
            }
        }

        if (!string.IsNullOrWhiteSpace(vm.DoctorId))
        {
            var bookedExams = await _db.chitietkhambenhs
                .Where(k => k.mabs == vm.DoctorId && k.ngaysudung >= dayStart && k.ngaysudung < dayEnd)
                .Select(k => k.ngaysudung)
                .ToListAsync();
            var bookedVacs = await _db.chitiettiemphongs
                .Where(k => k.mabs == vm.DoctorId && k.ngaytiem >= dayStart && k.ngaytiem < dayEnd)
                .Select(k => k.ngaytiem)
                .ToListAsync();

            var slotCounts = new Dictionary<string, int>();
            foreach (var slot in vm.Slots)
            {
                var slotTime = TimeSpan.Parse(slot.Id);
                var slotStart = dayStart.Add(slotTime);
                var slotEnd = slotStart.AddHours(1);
                var examCount = bookedExams.Count(t => t >= slotStart && t < slotEnd);
                var vacCount = bookedVacs.Count(t => t >= slotStart && t < slotEnd);
                var total = examCount + vacCount;
                if (total > 0) slotCounts[slot.Id] = total;
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
