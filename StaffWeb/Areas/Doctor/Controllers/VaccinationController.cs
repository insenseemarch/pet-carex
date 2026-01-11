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
        vm.ServiceId = vm.ServiceId?.Trim();
        vm.NewPackageServiceId = vm.NewPackageServiceId?.Trim();
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

            var branchServiceIds = new List<string>();
            if (!string.IsNullOrWhiteSpace(macn))
            {
                branchServiceIds = await _db.chinhanhs
                    .AsNoTracking()
                    .Where(x => x.macn == macn)
                    .SelectMany(x => x.madvs)
                    .Select(x => x.madv)
                    .ToListAsync();
            }

            if (string.Equals(vm.Mode, "Goi", StringComparison.OrdinalIgnoreCase))
            {
                if (vm.SelectedPendingStt.HasValue &&
                    !string.IsNullOrWhiteSpace(vm.SelectedPendingServiceId))
                {
                    var pending = await _db.chitiettiemphongs
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

                    pending.ngaytiem = vm.Date ?? DateTime.Now;
                    pending.trangthai = "Da tiem";
                    await _db.SaveChangesAsync();
                    TempData["Success"] = "Da ghi nhan dot tiem trong goi.";
                    return RedirectToAction(nameof(Create));
                }

                if (string.IsNullOrWhiteSpace(vm.NewPackageServiceId))
                {
                    ModelState.AddModelError("NewPackageServiceId", "Vui long chon goi tiem.");
                    return View(await BuildViewModel(vm, manv));
                }

                if (string.IsNullOrWhiteSpace(vm.VaccineId))
                {
                    ModelState.AddModelError("VaccineId", "Vui long chon vaccine.");
                    return View(await BuildViewModel(vm, manv));
                }

                if (branchServiceIds.Count > 0 && !branchServiceIds.Contains(vm.NewPackageServiceId))
                {
                    ModelState.AddModelError("NewPackageServiceId", "Goi tiem khong thuoc chi nhanh.");
                    return View(await BuildViewModel(vm, manv));
                }

                var pack = await _db.tiemgois
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.madv == vm.NewPackageServiceId);
                if (pack == null)
                {
                    ModelState.AddModelError("NewPackageServiceId", "Goi tiem khong ton tai.");
                    return View(await BuildViewModel(vm, manv));
                }

                var startDate = vm.PackageStartDate ?? DateTime.Now;
                var actualDate = vm.Date ?? DateTime.Now;
                var nextStt = (await _db.chitiettiemphongs.MaxAsync(x => (int?)x.stt) ?? 0) + 1;
                for (var i = 0; i < pack.sothang; i++)
                {
                    var planned = startDate.AddMonths(i);
                    var isFirst = i == 0;
                    _db.chitiettiemphongs.Add(new chitiettiemphong
                    {
                        stt = nextStt++,
                        madv = vm.NewPackageServiceId,
                        mathucung = vm.PetId,
                        mavacxin = vm.VaccineId,
                        mabs = manv,
                        ngaytiem = isFirst ? actualDate : planned,
                        trangthai = isFirst ? "Da tiem" : "Cho tiem"
                    });
                }

                await _db.SaveChangesAsync();
                TempData["Success"] = "Da tao goi tiem va ghi nhan dot dau tien.";
                return RedirectToAction(nameof(Create));
            }

            if (string.IsNullOrWhiteSpace(vm.ServiceId))
            {
                ModelState.AddModelError("ServiceId", "Vui long chon dich vu tiem.");
                return View(await BuildViewModel(vm, manv));
            }

            if (string.IsNullOrWhiteSpace(vm.VaccineId))
            {
                ModelState.AddModelError("VaccineId", "Vui long chon vaccine.");
                return View(await BuildViewModel(vm, manv));
            }

            if (branchServiceIds.Count > 0 && !branchServiceIds.Contains(vm.ServiceId))
            {
                ModelState.AddModelError("ServiceId", "Dich vu tiem khong thuoc chi nhanh.");
                return View(await BuildViewModel(vm, manv));
            }

            var nextSttSingle = (await _db.chitiettiemphongs.MaxAsync(x => (int?)x.stt) ?? 0) + 1;
            _db.chitiettiemphongs.Add(new chitiettiemphong
            {
                stt = nextSttSingle,
                madv = vm.ServiceId,
                mathucung = vm.PetId,
                mavacxin = vm.VaccineId,
                mabs = manv,
                ngaytiem = vm.Date ?? DateTime.Now,
                trangthai = "Da tiem"
            });

            await _db.SaveChangesAsync();
            TempData["Success"] = "Da ghi nhan tiem phong.";
            return RedirectToAction(nameof(Create));
        }
        catch (DbUpdateException ex)
        {
            var detail = ex.InnerException?.Message ?? ex.Message;
            ModelState.AddModelError("", "Khong the ghi nhan tiem phong. Vui long thu lai.");
            ModelState.AddModelError("", detail);
            return View(await BuildViewModel(vm, manv));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", "Khong the ghi nhan tiem phong. Vui long thu lai.");
            ModelState.AddModelError("", ex.Message);
            return View(await BuildViewModel(vm, manv));
        }
    }

    private async Task<VaccinationRecordViewModel> BuildViewModel(VaccinationRecordViewModel vm, string? manv)
    {
        var macn = string.IsNullOrWhiteSpace(manv)
            ? null
            : await _db.nhanviens
                .AsNoTracking()
                .Where(x => x.manv == manv)
                .Select(x => x.macn)
                .FirstOrDefaultAsync();

        var branchServiceIds = new List<string>();
        if (!string.IsNullOrWhiteSpace(macn))
        {
            branchServiceIds = await _db.chinhanhs
                .AsNoTracking()
                .Where(x => x.macn == macn)
                .SelectMany(x => x.madvs)
                .Select(x => x.madv)
                .ToListAsync();
        }

        var servicesQuery = _db.tiemphongs
            .Include(x => x.madvNavigation)
            .AsNoTracking();
        if (branchServiceIds.Count > 0)
        {
            servicesQuery = servicesQuery.Where(x => branchServiceIds.Contains(x.madv));
        }
        vm.Services = await servicesQuery
            .OrderBy(x => x.madvNavigation.tendv)
            .ToListAsync();

        var packageQuery = _db.tiemgois
            .Include(x => x.madvNavigation)
            .ThenInclude(x => x.madvNavigation)
            .AsNoTracking();
        if (branchServiceIds.Count > 0)
        {
            packageQuery = packageQuery.Where(x => branchServiceIds.Contains(x.madv));
        }
        vm.PackageOptions = await packageQuery
            .OrderBy(x => x.madvNavigation.madvNavigation.tendv)
            .Select(x => new VaccinationPackageOption
            {
                ServiceId = x.madv,
                Name = x.madvNavigation.madvNavigation.tendv,
                Months = x.sothang
            })
            .ToListAsync();

        vm.Vaccines = await _db.vacxins
            .AsNoTracking()
            .Where(x => x.ngayhethan >= DateOnly.FromDateTime(DateTime.Now))
            .OrderBy(x => x.tenvacxin)
            .ToListAsync();

        if (!string.IsNullOrWhiteSpace(vm.PetId))
        {
            vm.PendingPackages = await (
                from ct in _db.chitiettiemphongs.AsNoTracking()
                join tg in _db.tiemgois.AsNoTracking() on ct.madv equals tg.madv
                join dv in _db.dichvus.AsNoTracking() on ct.madv equals dv.madv into dvj
                from dv in dvj.DefaultIfEmpty()
                join vx in _db.vacxins.AsNoTracking() on ct.mavacxin equals vx.mavacxin into vxj
                from vx in vxj.DefaultIfEmpty()
                where ct.mathucung == vm.PetId &&
                      (ct.trangthai == null || ct.trangthai == "Cho tiem" || ct.trangthai == "Dang tiem")
                orderby (ct.trangthai == null || ct.trangthai == "Cho tiem" || ct.trangthai == "Dang tiem") ? 0 : 1,
                        ct.ngaytiem
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
