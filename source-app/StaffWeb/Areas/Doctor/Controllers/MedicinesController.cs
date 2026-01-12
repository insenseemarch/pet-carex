using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StaffWeb.Data;
using StaffWeb.Models;
using StaffWeb.Utils;

namespace StaffWeb.Areas.Doctor.Controllers;

[Area("Doctor")]
[Authorize(Roles = "Doctor")]
public class MedicinesController : Controller
{
    private readonly AppDbContext _db;

    public MedicinesController(AppDbContext db) => _db = db;

    public async Task<IActionResult> Index(string? q)
    {
        var vm = new MedicineSearchViewModel { Query = q };
        var items = await _db.sanphams.AsNoTracking().ToListAsync();
        var medicines = items
            .Where(x => TextUtil.Normalize(x.loaisp).Contains("thuoc"))
            .ToList();

        if (!string.IsNullOrWhiteSpace(q))
        {
            var nq = TextUtil.Normalize(q);
            medicines = medicines
                .Where(x =>
                    TextUtil.Normalize(x.tensp).Contains(nq) ||
                    TextUtil.Normalize(x.loaisp).Contains(nq))
                .ToList();
        }

        vm.Medicines = medicines.OrderBy(x => x.tensp).ToList();
        return View(vm);
    }
}
