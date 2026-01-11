using System.Security.Claims;
using System.Data;
using KhachHangWeb.Data;
using KhachHangWeb.Extensions;
using KhachHangWeb.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace KhachHangWeb.Controllers;

public class AccountController : Controller
{
    private const string StatusActive = "Đang hoạt động";
    private const string TierBasic = "Cơ bản";
    private const string TierFriendly = "Thân thiết";
    private const string TierVip = "VIP";
    private const string StatusPaid = "Đã thanh toán";

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
                x.trangthai == StatusActive);

        if (acc == null)
        {
            ModelState.AddModelError("", "Sai tài khoản hoặc mật khẩu.");
            return View(vm);
        }

        var updatedTier = await UpdateCustomerTierAsync(acc.makh, acc.capbac);
        acc.capbac = updatedTier;

        var fullName = await LoadCustomerFullNameAsync(acc.makh);

        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, acc.tendangnhap),
            new(ClaimTypes.Role, "Customer"),
            new("MaKhachHang", acc.makh),
            new("CapBac", acc.capbac ?? ""),
            new("Diem", acc.diemtichluy.ToString()),
            new("HoTen", fullName ?? "")
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
            if (!string.IsNullOrWhiteSpace(vm.Cccd))
            {
                await _db.Database.ExecuteSqlRawAsync(
                    "IF NOT EXISTS (SELECT 1 FROM thongtin WHERE cccd = @cccd) " +
                    "INSERT INTO thongtin (cccd, hoten, gioitinh, ngaysinh) VALUES (@cccd, @hoten, @gioitinh, @ngaysinh)",
                    new SqlParameter("@cccd", vm.Cccd),
                    new SqlParameter("@hoten", vm.FullName),
                    new SqlParameter("@gioitinh", (object?)vm.Gender ?? DBNull.Value),
                    new SqlParameter("@ngaysinh", vm.BirthDate.HasValue ? vm.BirthDate.Value.Date : DBNull.Value)
                );
            }

            _db.khachhangs.Add(new khachhang
            {
                makh = makh,
                sdt = vm.Phone,
                email = vm.Email,
                cccd = vm.Cccd
            });

            _db.taikhoankhachhangs.Add(new taikhoankhachhang
            {
                tendangnhap = vm.UserName,
                makh = makh,
                matkhau = vm.Password,
                diemtichluy = 0,
                capbac = TierBasic,
                trangthai = StatusActive
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
        _db.Database.SetCommandTimeout(60);
        var acc = await _db.taikhoankhachhangs
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.tendangnhap == userName);
        if (acc == null) return RedirectToAction("Login");

        var profile = await LoadCustomerProfileAsync(acc.makh);
        if (profile == null) return RedirectToAction("Login");

        var pets = await _db.thucungs
            .AsNoTracking()
            .Where(t => t.makh == acc.makh)
            .OrderBy(t => t.tenthucung)
            .ToListAsync();

        return View(new ProfileViewModel
        {
            Customer = profile,
            Pets = pets
        });
    }

    private static string NormalizeTier(string? tier)
    {
        if (string.IsNullOrWhiteSpace(tier)) return TierBasic;
        var value = tier.Trim().ToLowerInvariant();
        if (value.Contains("vip")) return TierVip;
        if (value.Contains("thân") || value.Contains("than")) return TierFriendly;
        if (value.Contains("cơ") || value.Contains("co")) return TierBasic;
        return TierBasic;
    }

    private async Task<string> UpdateCustomerTierAsync(string makh, string? currentTier)
    {
        var normalized = NormalizeTier(currentTier);
        var year = DateTime.Now.Year;
        var total = await _db.hoadons
            .AsNoTracking()
            .Where(h => h.makh == makh && h.trangthai == StatusPaid && h.ngaylap.Year == year)
            .SumAsync(h => (decimal?)h.thanhtien) ?? 0m;

        string newTier;
        if (total >= 12_000_000m)
            newTier = TierVip;
        else if (normalized == TierVip && total >= 8_000_000m)
            newTier = TierVip;
        else if (total >= 5_000_000m)
            newTier = TierFriendly;
        else if (normalized == TierFriendly && total >= 3_000_000m)
            newTier = TierFriendly;
        else
            newTier = TierBasic;

        if (!string.Equals(currentTier, newTier, StringComparison.Ordinal))
        {
            await _db.Database.ExecuteSqlRawAsync(
                "UPDATE taikhoankhachhang SET capbac = @capbac WHERE makh = @makh",
                new SqlParameter("@capbac", newTier),
                new SqlParameter("@makh", makh)
            );
        }

        return newTier;
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

    private async Task<CustomerProfileInfo?> LoadCustomerProfileAsync(string makh)
    {
        await using var conn = new SqlConnection(_db.Database.GetDbConnection().ConnectionString);
        await conn.OpenAsync();

        await using var cmd = new SqlCommand("sp_lay_thong_tin_khach_hang", conn)
        {
            CommandType = CommandType.StoredProcedure
        };
        cmd.Parameters.Add(new SqlParameter("@makh", makh));

        await using var reader = await cmd.ExecuteReaderAsync();
        if (!await reader.ReadAsync()) return null;

        return new CustomerProfileInfo
        {
            MaKh = reader.GetString(reader.GetOrdinal("makh")),
            Cccd = reader.IsDBNull(reader.GetOrdinal("cccd")) ? null : reader.GetString(reader.GetOrdinal("cccd")),
            HoTen = reader.IsDBNull(reader.GetOrdinal("hoten")) ? null : reader.GetString(reader.GetOrdinal("hoten")),
            GioiTinh = reader.IsDBNull(reader.GetOrdinal("gioitinh")) ? null : reader.GetString(reader.GetOrdinal("gioitinh")),
            NgaySinh = reader.IsDBNull(reader.GetOrdinal("ngaysinh")) ? null : reader.GetDateTime(reader.GetOrdinal("ngaysinh")),
            Sdt = reader.IsDBNull(reader.GetOrdinal("sdt")) ? null : reader.GetString(reader.GetOrdinal("sdt")),
            Email = reader.IsDBNull(reader.GetOrdinal("email")) ? null : reader.GetString(reader.GetOrdinal("email")),
            TenDangNhap = reader.IsDBNull(reader.GetOrdinal("tendangnhap")) ? null : reader.GetString(reader.GetOrdinal("tendangnhap")),
            DiemTichLuy = reader.IsDBNull(reader.GetOrdinal("diemtichluy")) ? 0 : reader.GetInt32(reader.GetOrdinal("diemtichluy")),
            CapBac = reader.IsDBNull(reader.GetOrdinal("capbac")) ? null : reader.GetString(reader.GetOrdinal("capbac")),
            TrangThai = reader.IsDBNull(reader.GetOrdinal("trangthai")) ? null : reader.GetString(reader.GetOrdinal("trangthai")),
            SoLuongThuCung = reader.IsDBNull(reader.GetOrdinal("soluongthucung")) ? 0 : reader.GetInt32(reader.GetOrdinal("soluongthucung"))
        };
    }

    private async Task<string?> LoadCustomerFullNameAsync(string makh)
    {
        await using var conn = new SqlConnection(_db.Database.GetDbConnection().ConnectionString);
        await conn.OpenAsync();

        await using var cmd = new SqlCommand(
            "SELECT tt.hoten FROM khachhang kh LEFT JOIN thongtin tt ON kh.cccd = tt.cccd WHERE kh.makh = @makh",
            conn
        );
        cmd.Parameters.Add(new SqlParameter("@makh", makh));

        var result = await cmd.ExecuteScalarAsync();
        return result == null || result == DBNull.Value ? null : result.ToString();
    }

    [HttpPost]
    public async Task<IActionResult> DeletePet(string petId)
    {
        var makh = User.FindFirst("MaKhachHang")?.Value;
        if (string.IsNullOrWhiteSpace(makh)) return RedirectToAction("Login");

        var pet = await _db.thucungs.FirstOrDefaultAsync(p => p.mathucung == petId && p.makh == makh);
        if (pet == null)
        {
            return Json(new { success = false, message = "Không tìm thấy thú cưng." });
        }

        try
        {
            _db.thucungs.Remove(pet);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Xóa thú cưng thành công." });
        }
        catch
        {
            return Json(new { success = false, message = "Xóa thú cưng thất bại. Vui lòng thử lại." });
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreatePet(string tenthucung, string loai, string giong, string gioitinh, string ngaysinh, string tinhtrangsuckhoe)
    {
        var makh = User.FindFirst("MaKhachHang")?.Value;
        if (string.IsNullOrWhiteSpace(makh)) return Json(new { success = false, message = "Vui lòng đăng nhập lại." });

        if (string.IsNullOrWhiteSpace(tenthucung) || string.IsNullOrWhiteSpace(loai) || string.IsNullOrWhiteSpace(giong))
        {
            return Json(new { success = false, message = "Vui lòng nhập đầy đủ thông tin bắt buộc." });
        }

        try
        {
            var mathucung = await GenerateMaThucung();
            
            var pet = new thucung
            {
                mathucung = mathucung,
                makh = makh,
                tenthucung = tenthucung,
                loai = loai,
                giong = giong,
                gioitinh = gioitinh,
                tinhtrangsuckhoe = tinhtrangsuckhoe
            };

            if (!string.IsNullOrWhiteSpace(ngaysinh) && DateTime.TryParse(ngaysinh, out var date))
            {
                pet.ngaysinh = DateOnly.FromDateTime(date);
            }

            _db.thucungs.Add(pet);
            await _db.SaveChangesAsync();

            return Json(new { success = true, message = "Thêm thú cưng thành công." });
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = $"Thêm thú cưng thất bại: {ex.Message}" });
        }
    }

    private async Task<string> GenerateMaThucung()
    {
        var last = await _db.thucungs
            .Where(p => p.mathucung.StartsWith("TC"))
            .OrderByDescending(p => p.mathucung)
            .Select(p => p.mathucung)
            .FirstOrDefaultAsync();

        if (string.IsNullOrWhiteSpace(last) || last.Length < 4)
            return "TC000001";

        if (!int.TryParse(last.Substring(2), out var num))
            return "TC000001";

        return $"TC{(num + 1):000000}";
    }

    [HttpGet]
    public async Task<IActionResult> EditPet(string petId)
    {
        var makh = User.FindFirst("MaKhachHang")?.Value;
        if (string.IsNullOrWhiteSpace(makh)) return RedirectToAction("Login");

        var pet = await _db.thucungs.FirstOrDefaultAsync(p => p.mathucung == petId && p.makh == makh);
        if (pet == null)
        {
            return Json(new { success = false, message = "Không tìm thấy thú cưng." });
        }

        return Json(new
        {
            success = true,
            pet = new
            {
                mathucung = pet.mathucung,
                tenthucung = pet.tenthucung,
                loai = pet.loai,
                giong = pet.giong,
                gioitinh = pet.gioitinh,
                ngaysinh = pet.ngaysinh?.ToString("yyyy-MM-dd"),
                tinhtrangsuckhoe = pet.tinhtrangsuckhoe
            }
        });
    }

    [HttpPost]
    public async Task<IActionResult> UpdatePet(string petId, string tenthucung, string loai, string giong, string gioitinh, string ngaysinh, string tinhtrangsuckhoe)
    {
        var makh = User.FindFirst("MaKhachHang")?.Value;
        if (string.IsNullOrWhiteSpace(makh)) return Json(new { success = false, message = "Vui lòng đăng nhập lại." });

        var pet = await _db.thucungs.FirstOrDefaultAsync(p => p.mathucung == petId && p.makh == makh);
        if (pet == null)
        {
            return Json(new { success = false, message = "Không tìm thấy thú cưng." });
        }

        try
        {
            pet.tenthucung = tenthucung;
            pet.loai = loai;
            pet.giong = giong;
            pet.gioitinh = gioitinh;
            
            if (!string.IsNullOrWhiteSpace(ngaysinh) && DateTime.TryParse(ngaysinh, out var date))
            {
                pet.ngaysinh = DateOnly.FromDateTime(date);
            }
            
            pet.tinhtrangsuckhoe = tinhtrangsuckhoe;

            _db.thucungs.Update(pet);
            await _db.SaveChangesAsync();

            return Json(new { success = true, message = "Cập nhật thú cưng thành công." });
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = $"Cập nhật thất bại: {ex.Message}" });
        }
    }
}
