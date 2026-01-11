using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StaffWeb.Data;
using StaffWeb.Models;
using StaffWeb.Utils;

namespace StaffWeb.Areas.Doctor.Controllers;

[Area("Doctor")]
[Authorize(Roles = "Doctor")]
public class RecordsController : Controller
{
    private readonly AppDbContext _db;

    public RecordsController(AppDbContext db) => _db = db;

    public async Task<IActionResult> Create()
    {
        var manv = User.FindFirstValue("MaNhanVien");
        var vm = await BuildViewModelAsync(new DoctorRecordViewModel(), manv);
        return View(vm);
    }

    public async Task<IActionResult> FromAppointment(string petId, string serviceId, long visitTicks)
    {
        var manv = User.FindFirstValue("MaNhanVien");
        if (string.IsNullOrWhiteSpace(manv))
        {
            return Forbid();
        }

        var visitTime = new DateTime(visitTicks);
        var appointment = await _db.chitietkhambenhs
            .Include(x => x.madvNavigation)
            .Include(x => x.mathucungNavigation)
            .AsNoTracking()
            .FirstOrDefaultAsync(x =>
                x.mabs == manv &&
                x.madv == serviceId &&
                x.mathucung == petId &&
                x.ngaysudung == visitTime);

        if (appointment == null)
        {
            return NotFound();
        }

        var vm = await BuildViewModelAsync(new DoctorRecordViewModel(), manv);
        vm.IsFromAppointment = true;
        vm.PetId = appointment.mathucung;
        vm.ServiceId = appointment.madv;
        vm.VisitTime = appointment.ngaysudung;
        vm.OriginalVisitTime = appointment.ngaysudung;
        vm.AppointmentTicks = visitTicks;
        vm.Symptoms = appointment.trieuchung;
        vm.Diagnosis = appointment.chandoan;
        vm.Notes = appointment.ghichu;
        vm.RecheckDate = appointment.ngaytaikham.HasValue
            ? appointment.ngaytaikham.Value.ToDateTime(TimeOnly.MinValue)
            : null;

        var pet = appointment.mathucungNavigation ?? await _db.thucungs
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.mathucung == appointment.mathucung);
        if (pet != null)
        {
            vm.PetName = pet.tenthucung;
            vm.PetType = pet.loai;
            vm.PetBreed = pet.giong;
            vm.PetGender = pet.gioitinh;
            vm.PetBirthDate = pet.ngaysinh;
            vm.PetHealth = pet.tinhtrangsuckhoe;
        }

        if (!string.IsNullOrWhiteSpace(appointment.matoathuoc))
        {
            var toa = await _db.toathuocs
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.matoathuoc == appointment.matoathuoc);
            if (toa != null)
            {
                vm.PrescriptionName = toa.tentoathuoc;
            }

            var details = await _db.thuocsudungs
                .AsNoTracking()
                .Where(x => x.matoathuoc == appointment.matoathuoc)
                .ToListAsync();
            vm.Items = details
                .Select(x => new PrescriptionItemViewModel
                {
                    MedicineId = x.masp,
                    Quantity = x.soluong,
                    Dosage = x.lieuluong,
                    Note = x.ghichu
                })
                .ToList();
            if (vm.Items.Count == 0)
            {
                vm.Items.Add(new PrescriptionItemViewModel());
            }
        }

        return View("Create", vm);
    }

    [HttpPost]
    public async Task<IActionResult> Create(DoctorRecordViewModel vm)
    {
        var manv = User.FindFirstValue("MaNhanVien");
        if (string.IsNullOrWhiteSpace(manv))
        {
            return Forbid();
        }

        vm.PetId = vm.PetId?.Trim();
        vm.ServiceId = vm.ServiceId?.Trim();
        vm.Items ??= new List<PrescriptionItemViewModel>();

        if (string.IsNullOrWhiteSpace(vm.PetId))
        {
            ModelState.AddModelError("PetId", "Ma thu cung bat buoc.");
        }
        if (string.IsNullOrWhiteSpace(vm.ServiceId))
        {
            ModelState.AddModelError("ServiceId", "Vui long chon dich vu.");
        }
        if (vm.VisitTime == null)
        {
            ModelState.AddModelError("VisitTime", "Vui long chon ngay gio kham.");
        }
        if (vm.RecheckDate.HasValue && vm.VisitTime.HasValue &&
            vm.RecheckDate.Value.Date <= vm.VisitTime.Value.Date)
        {
            ModelState.AddModelError("RecheckDate", "Ngay tai kham phai sau ngay kham.");
        }

        var hasMedicine = false;
        foreach (var item in vm.Items)
        {
            var hasMed = !string.IsNullOrWhiteSpace(item.MedicineId);
            var hasQty = (item.Quantity ?? 0) > 0;
            if (hasMed)
            {
                hasMedicine = true;
                if (!hasQty)
                {
                    ModelState.AddModelError("", "Vui long nhap so luong cho thuoc da chon.");
                    break;
                }
            }
            else if (hasQty)
            {
                ModelState.AddModelError("", "Vui long chon thuoc cho dong co so luong.");
                break;
            }
        }

        var macn = await _db.nhanviens
            .AsNoTracking()
            .Where(x => x.manv == manv)
            .Select(x => x.macn)
            .FirstOrDefaultAsync();
        if (!string.IsNullOrWhiteSpace(vm.ServiceId) && !string.IsNullOrWhiteSpace(macn))
        {
            var existsInBranch = await _db.chinhanhs
                .AsNoTracking()
                .Where(x => x.macn == macn)
                .SelectMany(x => x.madvs)
                .AnyAsync(d => d.madv == vm.ServiceId);
            if (!existsInBranch)
            {
                ModelState.AddModelError("ServiceId", "Dich vu khong thuoc chi nhanh cua bac si.");
            }
        }
        if (!ModelState.IsValid)
        {
            return View(await BuildViewModelAsync(vm, manv));
        }

        var petExists = await _db.thucungs.AnyAsync(x => x.mathucung == vm.PetId);
        if (!petExists)
        {
            ModelState.AddModelError("", "Ma thu cung khong ton tai.");
            return View(await BuildViewModelAsync(vm, manv));
        }

        string? toaId = null;
        var validItems = vm.Items
            .Where(x => !string.IsNullOrWhiteSpace(x.MedicineId) && (x.Quantity ?? 0) > 0)
            .ToList();

        if (vm.IsFromAppointment)
        {
            var appointmentTime = vm.AppointmentTicks.HasValue
                ? new DateTime(vm.AppointmentTicks.Value)
                : vm.VisitTime.Value;

            var appointment = await _db.chitietkhambenhs
                .FirstOrDefaultAsync(x =>
                    x.mabs == manv &&
                    x.madv == vm.ServiceId &&
                    x.mathucung == vm.PetId &&
                    x.ngaysudung == appointmentTime);

            if (appointment == null)
            {
                ModelState.AddModelError("", "Khong tim thay lich kham.");
                return View(await BuildViewModelAsync(vm, manv));
            }

            if (validItems.Count > 0)
            {
                if (!string.IsNullOrWhiteSpace(appointment.matoathuoc))
                {
                    toaId = appointment.matoathuoc;
                    var oldItems = _db.thuocsudungs
                        .Where(x => x.matoathuoc == appointment.matoathuoc);
                    _db.thuocsudungs.RemoveRange(oldItems);

                    var toa = await _db.toathuocs
                        .FirstOrDefaultAsync(x => x.matoathuoc == appointment.matoathuoc);
                    if (toa != null)
                    {
                        toa.tentoathuoc = string.IsNullOrWhiteSpace(vm.PrescriptionName)
                            ? $"Toa thuoc {vm.PetId}"
                            : vm.PrescriptionName;
                    }
                }
                else
                {
                    toaId = await GenerateToaId();
                    _db.toathuocs.Add(new toathuoc
                    {
                        matoathuoc = toaId,
                        tentoathuoc = string.IsNullOrWhiteSpace(vm.PrescriptionName)
                            ? $"Toa thuoc {vm.PetId}"
                            : vm.PrescriptionName
                    });
                }

                foreach (var item in validItems)
                {
                    _db.thuocsudungs.Add(new thuocsudung
                    {
                        matoathuoc = toaId!,
                        masp = item.MedicineId!,
                        soluong = item.Quantity ?? 1,
                        lieuluong = item.Dosage,
                        ghichu = item.Note
                    });
                }
            }
            else if (!string.IsNullOrWhiteSpace(appointment.matoathuoc))
            {
                var oldItems = _db.thuocsudungs
                    .Where(x => x.matoathuoc == appointment.matoathuoc);
                _db.thuocsudungs.RemoveRange(oldItems);

                var toa = await _db.toathuocs
                    .FirstOrDefaultAsync(x => x.matoathuoc == appointment.matoathuoc);
                if (toa != null)
                {
                    _db.toathuocs.Remove(toa);
                }
            }

            appointment.trieuchung = vm.Symptoms;
            appointment.chandoan = vm.Diagnosis;
            appointment.matoathuoc = validItems.Count > 0 ? toaId : null;
            appointment.ngaytaikham = vm.RecheckDate.HasValue
                ? DateOnly.FromDateTime(vm.RecheckDate.Value)
                : null;
            appointment.ghichu = vm.Notes;
        }
        else
        {
            if (validItems.Count > 0)
            {
                toaId = await GenerateToaId();
                _db.toathuocs.Add(new toathuoc
                {
                    matoathuoc = toaId,
                    tentoathuoc = string.IsNullOrWhiteSpace(vm.PrescriptionName)
                        ? $"Toa thuoc {vm.PetId}"
                        : vm.PrescriptionName
                });

                foreach (var item in validItems)
                {
                    _db.thuocsudungs.Add(new thuocsudung
                    {
                        matoathuoc = toaId,
                        masp = item.MedicineId!,
                        soluong = item.Quantity ?? 1,
                        lieuluong = item.Dosage,
                        ghichu = item.Note
                    });
                }
            }

            _db.chitietkhambenhs.Add(new chitietkhambenh
            {
                madv = vm.ServiceId,
                mathucung = vm.PetId,
                ngaysudung = vm.VisitTime.Value,
                mabs = manv,
                trieuchung = vm.Symptoms,
                chandoan = vm.Diagnosis,
                matoathuoc = toaId,
                ngaytaikham = vm.RecheckDate.HasValue
                    ? DateOnly.FromDateTime(vm.RecheckDate.Value)
                    : null,
                ghichu = vm.Notes
            });
        }

        try
        {
            await _db.SaveChangesAsync();
            TempData["Success"] = vm.IsFromAppointment ? "Da cap nhat lich kham." : "Da tao benh an.";
            if (vm.IsFromAppointment && vm.VisitTime.HasValue)
            {
                return RedirectToAction("Index", "Appointments", new
                {
                    area = "Doctor",
                    date = vm.VisitTime.Value.Date
                });
            }

            return RedirectToAction(nameof(Create));
        }
        catch (DbUpdateException ex)
        {
            var detail = ex.InnerException?.Message ?? ex.Message;
            ModelState.AddModelError("", "Khong the tao benh an. Vui long thu lai.");
            ModelState.AddModelError("", detail);
            return View(await BuildViewModelAsync(vm, manv));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", "Khong the tao benh an. Vui long thu lai.");
            ModelState.AddModelError("", ex.Message);
            return View(await BuildViewModelAsync(vm, manv));
        }
    }

    private async Task<DoctorRecordViewModel> BuildViewModelAsync(DoctorRecordViewModel vm, string? manv)
    {
        if (!string.IsNullOrWhiteSpace(manv))
        {
            var macn = await _db.nhanviens
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
        }

        if (vm.Services.Count == 0)
        {
            var allServices = await _db.dichvus
                .AsNoTracking()
                .OrderBy(x => x.tendv)
                .ToListAsync();
            vm.Services = allServices
                .Where(x => TextUtil.Normalize(x.loai).Contains("kham"))
                .ToList();
        }

        var items = await _db.sanphams.AsNoTracking().ToListAsync();
        vm.Medicines = items
            .Where(x => TextUtil.Normalize(x.loaisp).Contains("thuoc"))
            .OrderBy(x => x.tensp)
            .ToList();

        if (vm.VisitTime == null)
        {
            vm.VisitTime = DateTime.Now;
        }

        if (vm.Items.Count == 0)
        {
            vm.Items.Add(new PrescriptionItemViewModel());
        }

        return vm;
    }

    private async Task<string> GenerateToaId()
    {
        var last = await _db.toathuocs
            .AsNoTracking()
            .Where(t => t.matoathuoc.StartsWith("TT"))
            .OrderByDescending(t => t.matoathuoc)
            .Select(t => t.matoathuoc)
            .FirstOrDefaultAsync();

        var nextNum = 1;
        if (!string.IsNullOrWhiteSpace(last) &&
            last.Length >= 8 &&
            int.TryParse(last.Substring(2), out var num))
        {
            nextNum = num + 1;
        }

        while (true)
        {
            var candidate = $"TT{nextNum:000000}";
            var existsInDb = await _db.toathuocs
                .AsNoTracking()
                .AnyAsync(t => t.matoathuoc == candidate);
            var existsLocal = _db.toathuocs.Local.Any(t => t.matoathuoc == candidate);
            if (!existsInDb && !existsLocal)
            {
                return candidate;
            }

            nextNum++;
        }
    }
}
