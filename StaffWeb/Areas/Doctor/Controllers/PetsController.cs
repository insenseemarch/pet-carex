using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StaffWeb.Data;
using StaffWeb.Models;

namespace StaffWeb.Areas.Doctor.Controllers;

[Area("Doctor")]
[Authorize(Roles = "Doctor")]
public class PetsController : Controller
{
    private readonly AppDbContext _db;

    public PetsController(AppDbContext db) => _db = db;

    public async Task<IActionResult> Index(string? petId)
    {
        var vm = new DoctorSearchViewModel
        {
            PetId = petId
        };

        if (!string.IsNullOrWhiteSpace(petId))
        {
            vm.Pet = await _db.thucungs
                .Include(t => t.makhNavigation)
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.mathucung == petId);

            if (vm.Pet != null)
            {
                vm.Exams = await _db.chitietkhambenhs
                    .Include(x => x.madvNavigation)
                    .Include(x => x.mabsNavigation)
                    .AsNoTracking()
                    .Where(x => x.mathucung == petId)
                    .OrderByDescending(x => x.ngaysudung)
                    .ToListAsync();

                vm.Vaccines = await _db.chitiettiemphongs
                    .Include(x => x.madvNavigation)
                    .Include(x => x.mavacxinNavigation)
                    .Include(x => x.mabsNavigation)
                    .AsNoTracking()
                    .Where(x => x.mathucung == petId)
                    .OrderByDescending(x => x.ngaytiem)
                    .ToListAsync();
            }
        }

        return View(vm);
    }
}
