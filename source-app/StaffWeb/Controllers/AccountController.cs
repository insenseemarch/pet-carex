using System.Security.Claims;
using System.Linq;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StaffWeb.Data;
using StaffWeb.Models;

namespace StaffWeb.Controllers;

public class AccountController : Controller
{
    private readonly AppDbContext _db;

    public AccountController(AppDbContext db) => _db = db;

    public IActionResult Login() => View(new LoginViewModel());

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel vm)
    {
        if (!ModelState.IsValid)
        {
            return View(vm);
        }

        List<StaffLoginResult> result;
        try
        {
            result = await _db.Set<StaffLoginResult>()
                .FromSqlRaw("EXEC sp_dangnhap @tendangnhap={0}, @matkhau={1}", vm.UserName, vm.Password)
                .ToListAsync();
        }
        catch
        {
            ModelState.AddModelError("", "Sai tai khoan hoac mat khau.");
            return View(vm);
        }
        if (result.Count == 0)
        {
            ModelState.AddModelError("", "Sai tai khoan hoac mat khau.");
            return View(vm);
        }

        var login = result[0];
        if (!string.Equals(login.loai, "nhanvien", StringComparison.OrdinalIgnoreCase))
        {
            ModelState.AddModelError("", "Tai khoan khong duoc phep truy cap trang nhan vien.");
            return View(vm);
        }

        var role = MapRole(login.vaitro);
        if (string.IsNullOrWhiteSpace(role) && !string.IsNullOrWhiteSpace(login.manv))
        {
            var nv = await _db.nhanviens.AsNoTracking()
                .FirstOrDefaultAsync(x => x.manv == login.manv);
            role = MapRole(nv?.loainv) ?? MapRole(nv?.chucvu);
        }
        if (string.IsNullOrWhiteSpace(role))
        {
            ModelState.AddModelError("", "Khong xac dinh duoc vai tro.");
            return View(vm);
        }

        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, login.tendangnhap ?? vm.UserName),
            new(ClaimTypes.Role, role),
            new("MaNhanVien", login.manv ?? ""),
            new("MaChiNhanh", login.macn ?? "")
        };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

        return RedirectToArea(role);
    }

    public IActionResult Denied() => View();

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return RedirectToAction("Login", "Account", new { area = "" });
    }

    private static string? MapRole(string? vaitro)
    {
        if (string.IsNullOrWhiteSpace(vaitro))
        {
            return "";
        }

        var normalized = Utils.TextUtil.Normalize(vaitro);
        var compact = new string(normalized.Where(ch => !char.IsWhiteSpace(ch)).ToArray());
        if (normalized.Contains("bac si") || compact.Contains("bacsi") || compact.Contains("bacsithuy"))
        {
            return "Doctor";
        }

        if (normalized.Contains("tiep tan") || compact.Contains("tieptan"))
        {
            return "Staff";
        }

        if (normalized.Contains("quan ly") || compact.Contains("quanly") || compact.Contains("quanlychinhanh"))
        {
            return "Manager";
        }

        if (normalized.Contains("giam doc") || compact.Contains("giamdoc"))
        {
            return "Director";
        }

        return "";
    }

    private IActionResult RedirectToArea(string role)
    {
        var area = role switch
        {
            "Doctor" => "Doctor",
            "Staff" => "Staff",
            "Manager" => "Manager",
            "Director" => "Director",
            _ => ""
        };

        return RedirectToAction("Index", "Home", new { area });
    }
}
