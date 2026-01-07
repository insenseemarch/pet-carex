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
public class LookupController : Controller
{
    private readonly AppDbContext _db;

    public LookupController(AppDbContext db) => _db = db;

    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> Pet(string? petId)
    {
        var vm = new PetHistoryLookupViewModel { PetId = petId };

        if (!string.IsNullOrWhiteSpace(petId))
        {
            vm.Pet = await _db.thucungs
                .Include(t => t.makhNavigation)
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.mathucung == petId);

            vm.Customer = vm.Pet?.makhNavigation;

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

    public async Task<IActionResult> DoctorHistory(string? petId, DateTime? from, DateTime? to)
    {
        var manv = User.FindFirstValue("MaNhanVien");
        if (string.IsNullOrWhiteSpace(manv))
        {
            return Forbid();
        }

        var vm = new DoctorHistoryViewModel
        {
            Doctor = await _db.nhanviens
                .Include(x => x.macnNavigation)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.manv == manv),
            Filter = new DoctorSearchFilter
            {
                PetId = petId,
                From = from,
                To = to
            }
        };

        var examsQuery = _db.chitietkhambenhs
            .Include(x => x.madvNavigation)
            .Include(x => x.mathucungNavigation)
            .AsNoTracking()
            .Where(x => x.mabs == manv);

        if (!string.IsNullOrWhiteSpace(petId))
        {
            examsQuery = examsQuery.Where(x => x.mathucung == petId);
        }
        if (from.HasValue)
        {
            examsQuery = examsQuery.Where(x => x.ngaysudung >= from.Value);
        }
        if (to.HasValue)
        {
            var toDate = to.Value.Date.AddDays(1).AddSeconds(-1);
            examsQuery = examsQuery.Where(x => x.ngaysudung <= toDate);
        }

        vm.Exams = await examsQuery
            .OrderByDescending(x => x.ngaysudung)
            .ToListAsync();

        var vaccQuery = _db.chitiettiemphongs
            .Include(x => x.madvNavigation)
            .ThenInclude(tp => tp.madvNavigation)
            .Include(x => x.mavacxinNavigation)
            .Include(x => x.mathucungNavigation)
            .AsNoTracking()
            .Where(x => x.mabs == manv);

        if (!string.IsNullOrWhiteSpace(petId))
        {
            vaccQuery = vaccQuery.Where(x => x.mathucung == petId);
        }
        if (from.HasValue)
        {
            vaccQuery = vaccQuery.Where(x => x.ngaytiem >= DateOnly.FromDateTime(from.Value));
        }
        if (to.HasValue)
        {
            vaccQuery = vaccQuery.Where(x => x.ngaytiem <= DateOnly.FromDateTime(to.Value));
        }

        vm.Vaccines = await vaccQuery
            .OrderByDescending(x => x.ngaytiem)
            .ToListAsync();

        return View(vm);
    }

    public async Task<IActionResult> Vaccines(string? q, string? petId)
    {
        var vm = new VaccineLookupViewModel { Query = q, PetId = petId };

        var vaccines = await _db.vacxins.AsNoTracking().ToListAsync();
        if (!string.IsNullOrWhiteSpace(q))
        {
            var nq = TextUtil.Normalize(q);
            vaccines = vaccines
                .Where(x =>
                    TextUtil.Normalize(x.tenvacxin).Contains(nq) ||
                    TextUtil.Normalize(x.loaivacxin ?? "").Contains(nq))
                .ToList();
        }
        vm.Vaccines = vaccines.OrderBy(x => x.tenvacxin).ToList();

        if (!string.IsNullOrWhiteSpace(petId))
        {
            vm.PetVaccines = await _db.chitiettiemphongs
                .Include(x => x.mavacxinNavigation)
                .Include(x => x.madvNavigation)
                .ThenInclude(tp => tp.madvNavigation)
                .Include(x => x.mathucungNavigation)
                .Include(x => x.mabsNavigation)
                .AsNoTracking()
                .Where(x => x.mathucung == petId)
                .OrderByDescending(x => x.ngaytiem)
                .ToListAsync();
        }

        return View(vm);
    }

}
