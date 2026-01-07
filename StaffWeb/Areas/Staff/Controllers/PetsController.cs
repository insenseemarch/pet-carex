using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StaffWeb.Data;
using StaffWeb.Models;

namespace StaffWeb.Areas.Staff.Controllers;

[Area("Staff")]
[Authorize(Roles = "Staff")]
public class PetsController : Controller
{
    private readonly AppDbContext _db;

    public PetsController(AppDbContext db) => _db = db;

    public async Task<IActionResult> Index(string? petId, string? phone)
    {
        var vm = new PetLookupViewModel
        {
            PetId = petId,
            Phone = phone
        };

        if (!string.IsNullOrWhiteSpace(petId))
        {
            vm.Pet = await _db.thucungs
                .Include(t => t.makhNavigation)
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.mathucung == petId);
            vm.Customer = vm.Pet?.makhNavigation;
        }

        if (!string.IsNullOrWhiteSpace(phone))
        {
            vm.Customer = await _db.khachhangs
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.sdt == phone);

            if (vm.Customer != null)
            {
                vm.PetsByCustomer = await _db.thucungs
                    .AsNoTracking()
                    .Where(x => x.makh == vm.Customer.makh)
                    .OrderBy(x => x.tenthucung)
                    .ToListAsync();
            }
        }

        return View(vm);
    }
}
