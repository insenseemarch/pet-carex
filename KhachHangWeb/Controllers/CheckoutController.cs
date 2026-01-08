using KhachHangWeb.Data;
using KhachHangWeb.Extensions;
using KhachHangWeb.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace KhachHangWeb.Controllers;

[Authorize]
public class CheckoutController : Controller
{
    private readonly AppDbContext _db;
    public CheckoutController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var cart = HttpContext.Session.GetObject<List<CartItem>>("CART") ?? new();
        if (!cart.Any()) return View(new CheckoutViewModel { Items = cart });

        var makh = User.FindFirst("MaKhachHang")?.Value;
        if (string.IsNullOrWhiteSpace(makh)) return RedirectToAction("Login", "Account");

        var memberTier = User.FindFirst("CapBac")?.Value;
        var points = ParsePoints(User.FindFirst("Diem")?.Value);
        var vm = await BuildCheckoutViewModel(cart, makh, memberTier, points, null, "Tien mat", "", false);
        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> PlaceOrder(
        string? promoCode,
        string? paymentMethod,
        string? branchId,
        bool usePoints = false,
        bool payNow = false,
        bool recalc = false)
    {
        var cart = HttpContext.Session.GetObject<List<CartItem>>("CART") ?? new();
        if (!cart.Any()) return RedirectToAction("Index", "Cart");

        var makh = User.FindFirst("MaKhachHang")?.Value;
        if (string.IsNullOrWhiteSpace(makh)) return RedirectToAction("Login", "Account");

        paymentMethod ??= "Tien mat";
        branchId ??= "";
        promoCode = "";

        if (!payNow) usePoints = false;

        var memberTier = User.FindFirst("CapBac")?.Value;
        var points = ParsePoints(User.FindFirst("Diem")?.Value);
        var vm = await BuildCheckoutViewModel(cart, makh, memberTier, points, branchId, paymentMethod, promoCode, usePoints);
        if (recalc)
        {
            return View("Index", vm);
        }

        var hasCustomer = await _db.khachhangs
            .AsNoTracking()
            .AnyAsync(k => k.makh == makh);
        if (!hasCustomer)
        {
            ModelState.AddModelError("", "Khong tim thay thong tin khach hang. Vui long dang ky tai khoan lai.");
            return View("Index", vm);
        }

        var ids = cart.Select(c => c.MaSP).ToList();
        var products = await _db.sanphams.Where(p => ids.Contains(p.masp)).ToListAsync();
        foreach (var c in cart)
        {
            var p = products.FirstOrDefault(x => x.masp == c.MaSP);
            if (p == null || p.soluongton < c.Quantity)
            {
                ModelState.AddModelError("", $"Het hang: {c.Name}");
                return View("Index", vm);
            }
        }

        var mahd = await GenerateHoaDonId();
        var tongtien = vm.Total;
        var khuyenmai = vm.Discount;

        await using var tx = await _db.Database.BeginTransactionAsync();
        try
        {
            await _db.Database.ExecuteSqlRawAsync(
                "EXEC sp_lap_hoadon @mahd, @mathucung, @manvlap, @macn, @makh, @tongtien, @khuyenmai",
                new SqlParameter("@mahd", mahd),
                new SqlParameter("@mathucung", DBNull.Value),
                new SqlParameter("@manvlap", DBNull.Value),
                new SqlParameter("@macn", DBNull.Value),
                new SqlParameter("@makh", makh),
                new SqlParameter("@tongtien", tongtien),
                new SqlParameter("@khuyenmai", khuyenmai)
            );

            foreach (var c in cart)
            {
                var thanhtien = c.UnitPrice * c.Quantity;
                await _db.Database.ExecuteSqlRawAsync(
                    "INSERT INTO chitietmuasanpham (mahd, masp, soluong, thanhtien) VALUES (@mahd, @masp, @soluong, @thanhtien)",
                    new SqlParameter("@mahd", mahd),
                    new SqlParameter("@masp", c.MaSP),
                    new SqlParameter("@soluong", c.Quantity),
                    new SqlParameter("@thanhtien", thanhtien)
                );
            }

            if (payNow)
            {
                await _db.Database.ExecuteSqlRawAsync(
                    "EXEC sp_thanhtoan_hoadon @mahd, @hinhthucthanhtoan",
                    new SqlParameter("@mahd", mahd),
                    new SqlParameter("@hinhthucthanhtoan", paymentMethod)
                );

                if (vm.PointsUsed > 0)
                {
                    await _db.Database.ExecuteSqlRawAsync(
                        "UPDATE taikhoankhachhang SET diemtichluy = CASE WHEN diemtichluy >= @used THEN diemtichluy - @used ELSE 0 END WHERE makh = @makh",
                        new SqlParameter("@used", vm.PointsUsed),
                        new SqlParameter("@makh", makh)
                    );
                }

                await UpdateCustomerTierAsync(makh);
            }

            await tx.CommitAsync();

            if (payNow)
            {
                await RefreshUserClaimsAsync(makh);
            }
        }
        catch (Exception ex)
        {
            await tx.RollbackAsync();
            var detail = ex.InnerException?.Message ?? ex.Message;
            Console.Error.WriteLine(ex.ToString());
            ModelState.AddModelError("", $"Khong the tao hoa don: {detail}");
            return View("Index", vm);
        }

        HttpContext.Session.SetObject("CART", new List<CartItem>());
        TempData["OrderId"] = mahd;
        return RedirectToAction("Success");
    }

    public IActionResult Success()
    {
        ViewBag.OrderId = TempData["OrderId"];
        return View();
    }

    private async Task<CheckoutViewModel> BuildCheckoutViewModel(
        List<CartItem> cart,
        string makh,
        string? memberTier,
        int pointsAvailable,
        string? branchId,
        string paymentMethod,
        string promoCode,
        bool usePoints)
    {
        _db.Database.SetCommandTimeout(60);
        var total = cart.Sum(x => x.UnitPrice * x.Quantity);
        var promoDiscount = CalculateDiscount(total, promoCode);

        var normalizedTier = NormalizeTier(memberTier);
        var memberDiscountPercent = GetMemberDiscountPercent(normalizedTier);
        var memberDiscountAmount = Math.Round(total * memberDiscountPercent, 0);

        var subtotalAfterMemberPromo = total - memberDiscountAmount - promoDiscount;
        if (subtotalAfterMemberPromo < 0)
            subtotalAfterMemberPromo = 0;

        var pointsUsed = 0;
        var pointsDiscountAmount = 0m;
        if (usePoints && pointsAvailable > 0)
        {
            pointsUsed = (int)Math.Min(pointsAvailable, Math.Floor(subtotalAfterMemberPromo / 1000m));
            pointsDiscountAmount = pointsUsed * 1000m;
        }

        var discount = promoDiscount + memberDiscountAmount + pointsDiscountAmount;
        var payable = total - discount;
        if (payable < 0)
            payable = 0;

        var selectedBranch = string.IsNullOrWhiteSpace(branchId) ? "" : branchId;

        return new CheckoutViewModel
        {
            Items = cart,
            Branches = new List<BranchOption>(),
            SelectedBranch = selectedBranch,
            PaymentMethod = string.IsNullOrWhiteSpace(paymentMethod) ? "Tien mat" : paymentMethod,
            PromoCode = promoCode,
            Total = total,
            Discount = discount,
            Payable = payable,
            DefaultPetId = null,
            DefaultPetName = null,
            MemberTier = normalizedTier,
            MemberDiscountPercent = memberDiscountPercent,
            MemberDiscountAmount = memberDiscountAmount,
            PointsAvailable = pointsAvailable,
            PointsUsed = pointsUsed,
            PointsDiscountAmount = pointsDiscountAmount,
            UsePoints = usePoints
        };
    }

    private static string NormalizeTier(string? tier)
    {
        if (string.IsNullOrWhiteSpace(tier)) return "Cơ bản";
        var value = tier.Trim().ToLowerInvariant();
        if (value.Contains("vip")) return "VIP";
        if (value.Contains("thân") || value.Contains("than")) return "Thân thiết";
        if (value.Contains("cơ") || value.Contains("co")) return "Cơ bản";
        return "Cơ bản";
    }

private static decimal GetMemberDiscountPercent(string tier)
    {
        return tier switch
        {
            "VIP" => 0.15m,
            "Thân thiết" => 0.10m,
            _ => 0m
        };
    }

private static decimal CalculateDiscount(decimal total, string promoCode)
    {
        if (string.IsNullOrWhiteSpace(promoCode)) return 0;

        return promoCode.Trim().ToUpperInvariant() switch
        {
            "KM10" => Math.Min(total * 0.10m, 100_000m),
            "KM50K" => Math.Min(50_000m, total),
            _ => 0
        };
    }

    private static int ParsePoints(string? value)
    {
        return int.TryParse(value, out var points) ? points : 0;
    }

    private async Task<string> GenerateHoaDonId()
    {
        const string prefix = "HD";
        await using var conn = new SqlConnection(_db.Database.GetDbConnection().ConnectionString);
        await conn.OpenAsync();

        await using var cmd = new SqlCommand(
            "SELECT TOP(1) mahd FROM hoadon WITH (NOLOCK) WHERE mahd LIKE 'HD%' ORDER BY mahd DESC",
            conn
        )
        {
            CommandTimeout = 10
        };

        try
        {
            var result = await cmd.ExecuteScalarAsync();
            if (result != null && result != DBNull.Value)
            {
                var lastId = result.ToString() ?? "";
                if (lastId.Length >= 3 && int.TryParse(lastId.Substring(2), out var current))
                {
                    return $"{prefix}{current + 1:0000000}";
                }
            }
        }
        catch
        {
            // Fallback below.
        }

        var fallback = Random.Shared.Next(1_000_000, 9_999_999);
        return $"{prefix}{fallback:0000000}";
    }

    private async Task UpdateCustomerTierAsync(string makh)
    {
        var acc = await _db.taikhoankhachhangs
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.makh == makh);
        if (acc == null) return;

        var normalized = NormalizeTier(acc.capbac);
        var year = DateTime.Now.Year;
        var total = await _db.hoadons
            .AsNoTracking()
            .Where(h => h.makh == makh && h.trangthai == "Đã thanh toán" && h.ngaylap.Year == year)
            .SumAsync(h => (decimal?)h.thanhtien) ?? 0m;

        string newTier;
        if (total >= 12_000_000m)
            newTier = "VIP";
        else if (normalized == "VIP" && total >= 8_000_000m)
            newTier = "VIP";
        else if (total >= 5_000_000m)
            newTier = "Thân thiết";
        else if (normalized == "Thân thiết" && total >= 3_000_000m)
            newTier = "Thân thiết";
        else
            newTier = "Cơ bản";

        if (!string.Equals(acc.capbac, newTier, StringComparison.Ordinal))
        {
            await _db.Database.ExecuteSqlRawAsync(
                "UPDATE taikhoankhachhang SET capbac = @capbac WHERE makh = @makh",
                new SqlParameter("@capbac", newTier),
                new SqlParameter("@makh", makh)
            );
        }
    }

private async Task RefreshUserClaimsAsync(string makh)
    {
        try
        {
            var acc = await _db.taikhoankhachhangs
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.makh == makh);
            if (acc == null) return;

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
        }
        catch
        {
            // Ignore claim refresh failures.
        }
    }
}
