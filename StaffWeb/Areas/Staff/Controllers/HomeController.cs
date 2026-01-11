using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StaffWeb.Data;
using StaffWeb.Models;

namespace StaffWeb.Areas.Staff.Controllers;

[Area("Staff")]
[Authorize(Roles = "Staff")]
public class HomeController : Controller
{
    private readonly AppDbContext _db;

    public HomeController(AppDbContext db) => _db = db;

    public async Task<IActionResult> Index()
    {
        var manv = User.FindFirstValue("MaNhanVien");
        nhanvien? staff = null;
        chinhanh? branch = null;

        if (!string.IsNullOrWhiteSpace(manv))
        {
            staff = await _db.nhanviens
                .Include(x => x.macnNavigation)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.manv == manv);
            branch = staff?.macnNavigation;
        }

        var vm = new StaffDashboardViewModel
        {
            Staff = staff,
            Branch = branch
        };

        return View(vm);
    }
}
