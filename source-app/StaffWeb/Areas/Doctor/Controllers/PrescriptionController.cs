using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StaffWeb.Data;
using StaffWeb.Models;
using StaffWeb.Utils;

namespace StaffWeb.Areas.Doctor.Controllers;

[Area("Doctor")]
[Authorize(Roles = "Doctor")]
public class PrescriptionController : Controller
{
    private readonly AppDbContext _db;

    public PrescriptionController(AppDbContext db) => _db = db;

    public async Task<IActionResult> Create()
    {
        var vm = await BuildViewModel(new PrescriptionViewModel());
        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> Create(PrescriptionViewModel vm)
    {
        vm.PetId = vm.PetId?.Trim();
        vm.PrescriptionName = vm.PrescriptionName?.Trim();

        var validItems = vm.Items
            .Where(x => !string.IsNullOrWhiteSpace(x.MedicineId) && (x.Quantity ?? 0) > 0)
            .ToList();

        if (validItems.Count == 0)
        {
            ModelState.AddModelError("", "Can chon it nhat 1 thuoc va so luong.");
            return View(await BuildViewModel(vm));
        }

        if (!string.IsNullOrWhiteSpace(vm.PetId))
        {
            var petExists = await _db.thucungs.AnyAsync(x => x.mathucung == vm.PetId);
            if (!petExists)
            {
                ModelState.AddModelError("PetId", "Ma thu cung khong ton tai.");
                return View(await BuildViewModel(vm));
            }
        }

        var toaId = await GenerateToaId();
        var fallbackKey = !string.IsNullOrWhiteSpace(vm.PetId)
            ? vm.PetId
            : DateTime.Now.ToString("yyyyMMddHHmm");
        var name = !string.IsNullOrWhiteSpace(vm.PrescriptionName)
            ? vm.PrescriptionName
            : $"Toa thuoc {fallbackKey}";

        _db.toathuocs.Add(new toathuoc
        {
            matoathuoc = toaId,
            tentoathuoc = name
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

        try
        {
            await _db.SaveChangesAsync();
            TempData["Success"] = "Da tao toa thuoc.";
            return RedirectToAction(nameof(Create));
        }
        catch
        {
            ModelState.AddModelError("", "Khong the tao toa thuoc. Vui long thu lai.");
            return View(await BuildViewModel(vm));
        }
    }

    private async Task<PrescriptionViewModel> BuildViewModel(PrescriptionViewModel vm)
    {
        var items = await _db.sanphams.AsNoTracking().ToListAsync();
        vm.Medicines = items
            .Where(x => TextUtil.Normalize(x.loaisp).Contains("thuoc"))
            .OrderBy(x => x.tensp)
            .ToList();

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
