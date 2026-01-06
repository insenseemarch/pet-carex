using System.Security.Claims;
using KhachHangWeb.Data;
using KhachHangWeb.Extensions;
using KhachHangWeb.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KhachHangWeb.Controllers;

public class AccountController : Controller
{
    private readonly AppDbContext _db;
    public AccountController(AppDbContext db) => _db = db;

    public IActionResult Login() => View(new LoginViewModel());

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel vm)
    {
        var acc = await _db.taikhoankhachhangs
            .Include(x => x.makhNavigation)
            .FirstOrDefaultAsync(x =>
                x.tendangnhap == vm.UserName &&
                x.matkhau == vm.Password &&
                x.trangthai == "Đang hoạt động");

        if (acc == null)
        {
            ModelState.AddModelError("", "Sai tài khoản hoặc mật khẩu");
            return View(vm);
        }

        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, acc.tendangnhap),
            new(ClaimTypes.Role, "Customer"),
            new("MaKhachHang", acc.makh),
            new("CapBac", acc.capbac ?? ""),
            new("Diem", acc.diemtichluy.ToString())
        };
        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        HttpContext.Session.SetObject("CART", new List<CartItem>());
        return RedirectToAction("Index", "Home");
    }

    public IActionResult Register() => View(new RegisterViewModel());

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel vm)
    {
        if (string.IsNullOrWhiteSpace(vm.FullName) || string.IsNullOrWhiteSpace(vm.Phone) ||
            string.IsNullOrWhiteSpace(vm.UserName) || string.IsNullOrWhiteSpace(vm.Password))
        {
            ModelState.AddModelError("", "Vui lòng nhập đầy đủ thông tin bắt buộc.");
            return View(vm);
        }

        if (vm.Password != vm.ConfirmPassword)
        {
            ModelState.AddModelError("", "Mật khẩu không khớp.");
            return View(vm);
        }

        if (await _db.taikhoankhachhangs.AnyAsync(x => x.tendangnhap == vm.UserName))
        {
            ModelState.AddModelError("", "Tên đăng nhập đã tồn tại.");
            return View(vm);
        }

        if (await _db.khachhangs.AnyAsync(x => x.sdt == vm.Phone))
        {
            ModelState.AddModelError("", "Số điện thoại đã tồn tại.");
            return View(vm);
        }

        if (!string.IsNullOrWhiteSpace(vm.Email) &&
            await _db.khachhangs.AnyAsync(x => x.email == vm.Email))
        {
            ModelState.AddModelError("", "Email đã tồn tại.");
            return View(vm);
        }

        if (!string.IsNullOrWhiteSpace(vm.Cccd) &&
            await _db.khachhangs.AnyAsync(x => x.cccd == vm.Cccd))
        {
            ModelState.AddModelError("", "CCCD đã tồn tại.");
            return View(vm);
        }

        var makh = await GenerateMaKh();

        await using var tx = await _db.Database.BeginTransactionAsync();
        try
        {
            _db.khachhangs.Add(new khachhang
            {
                makh = makh,
                hoten = vm.FullName,
                sdt = vm.Phone,
                email = vm.Email,
                cccd = vm.Cccd,
                gioitinh = vm.Gender,
                ngaysinh = vm.BirthDate.HasValue ? DateOnly.FromDateTime(vm.BirthDate.Value) : null
            });

            _db.taikhoankhachhangs.Add(new taikhoankhachhang
            {
                tendangnhap = vm.UserName,
                makh = makh,
                matkhau = vm.Password,
                diemtichluy = 0,
                capbac = "Cơ bản",
                trangthai = "Đang hoạt động"
            });

            await _db.SaveChangesAsync();
            await tx.CommitAsync();
        }
        catch
        {
            await tx.RollbackAsync();
            ModelState.AddModelError("", "Đăng ký thất bại. Vui lòng thử lại.");
            return View(vm);
        }

        return RedirectToAction("Login");
    }

    public async Task<IActionResult> Profile()
    {
        var userName = User.Identity?.Name ?? "";
        var acc = await _db.taikhoankhachhangs
            .Include(x => x.makhNavigation)
            .FirstOrDefaultAsync(x => x.tendangnhap == userName);
        if (acc == null) return RedirectToAction("Login");

        var pets = await _db.thucungs
            .Where(t => t.makh == acc.makh)
            .OrderBy(t => t.tenthucung)
            .ToListAsync();

        return View(new ProfileViewModel
        {
            Customer = acc.makhNavigation,
            Pets = pets
        });
    }

    private async Task<string> GenerateMaKh()
    {
        var last = await _db.khachhangs
            .Where(k => k.makh.StartsWith("KH"))
            .OrderByDescending(k => k.makh)
            .Select(k => k.makh)
            .FirstOrDefaultAsync();

        if (string.IsNullOrWhiteSpace(last) || last.Length < 4)
            return "KH000001";

        if (!int.TryParse(last.Substring(2), out var num))
            return "KH000001";

        return $"KH{(num + 1):000000}";
    }
}
