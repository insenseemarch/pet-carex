using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StaffWeb.Data;
using StaffWeb.Models;

namespace StaffWeb.Areas.Doctor.Controllers;

[Area("Doctor")]
[Authorize(Roles = "Doctor")]
public class HomeController : Controller
{
    private readonly AppDbContext _db;

    public HomeController(AppDbContext db) => _db = db;

    public async Task<IActionResult> Index(string? tab)
    {
        var manv = User.FindFirstValue("MaNhanVien");
        nhanvien? doctor = null;
        chinhanh? branch = null;

        if (!string.IsNullOrWhiteSpace(manv))
        {
            doctor = await _db.nhanviens
                .Include(x => x.macnNavigation)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.manv == manv);
            branch = doctor?.macnNavigation;
        }

        var vm = new DoctorDashboardViewModel
        {
            Doctor = doctor,
            Branch = branch,
            ShowSearchBoxes = string.Equals(tab, "search", StringComparison.OrdinalIgnoreCase)
        };

        return View(vm);
    }
}
