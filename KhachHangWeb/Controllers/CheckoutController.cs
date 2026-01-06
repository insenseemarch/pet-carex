using KhachHangWeb.Data;
using KhachHangWeb.Extensions;
using KhachHangWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

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

        var vm = await BuildCheckoutViewModel(cart, makh, null, "Tiền mặt", "");
        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> PlaceOrder(string? promoCode, string? paymentMethod, string? branchId, bool payNow = false)
    {
        var cart = HttpContext.Session.GetObject<List<CartItem>>("CART") ?? new();
        if (!cart.Any()) return RedirectToAction("Index", "Cart");

        var makh = User.FindFirst("MaKhachHang")?.Value;
        if (string.IsNullOrWhiteSpace(makh)) return RedirectToAction("Login", "Account");

        paymentMethod ??= "Tiền mặt";
        branchId ??= "";
        promoCode ??= "";

        var vm = await BuildCheckoutViewModel(cart, makh, branchId, paymentMethod, promoCode);
        if (!vm.Branches.Any())
        {
            ModelState.AddModelError("", "Không có chi nhánh phù hợp với giỏ hàng.");
            return View("Index", vm);
        }

        if (string.IsNullOrWhiteSpace(vm.SelectedBranch))
        {
            ModelState.AddModelError("", "Vui lòng chọn chi nhánh.");
            return View("Index", vm);
        }

        if (string.IsNullOrWhiteSpace(vm.DefaultPetId))
        {
            ModelState.AddModelError("", "Khách hàng chưa có thú cưng.");
            return View("Index", vm);
        }

        var manvlap = await _db.nhanviens
            .Where(n => n.macn == vm.SelectedBranch && n.loainv == "Nhân viên tiếp tân")
            .OrderBy(n => n.manv)
            .Select(n => n.manv)
            .FirstOrDefaultAsync();

        if (string.IsNullOrWhiteSpace(manvlap))
        {
            ModelState.AddModelError("", "Không tìm thấy nhân viên tiếp tân cho chi nhánh.");
            return View("Index", vm);
        }

        var ids = cart.Select(c => c.MaSP).ToList();
        var products = await _db.sanphams.Where(p => ids.Contains(p.masp)).ToListAsync();
        foreach (var c in cart)
        {
            var p = products.FirstOrDefault(x => x.masp == c.MaSP);
            if (p == null || p.soluongton < c.Quantity)
            {
                ModelState.AddModelError("", $"Hết hàng: {c.Name}");
                return View("Index", vm);
            }
        }

        var mahd = Guid.NewGuid().ToString("N")[..10];
        var tongtien = vm.Total;
        var khuyenmai = vm.Discount;

        await using var tx = await _db.Database.BeginTransactionAsync();
        try
        {
            await _db.Database.ExecuteSqlRawAsync(
                "EXEC sp_lap_hoadon @mahd, @mathucung, @manvlap, @macn, @makh, @tongtien, @khuyenmai",
                new SqlParameter("@mahd", mahd),
                new SqlParameter("@mathucung", vm.DefaultPetId),
                new SqlParameter("@manvlap", manvlap),
                new SqlParameter("@macn", vm.SelectedBranch),
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
            }

            await tx.CommitAsync();
        }
        catch
        {
            await tx.RollbackAsync();
            ModelState.AddModelError("", "Không thể tạo hóa đơn. Vui lòng thử lại.");
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
        string? branchId,
        string paymentMethod,
        string promoCode)
    {
        var total = cart.Sum(x => x.UnitPrice * x.Quantity);
        var discount = CalculateDiscount(total, promoCode);

        var cartIds = cart.Select(c => c.MaSP).ToList();
        var cartCount = cartIds.Count;

        var branches = await _db.chinhanhs
            .Where(c => c.masps
                .Where(p => cartIds.Contains(p.masp))
                .Select(p => p.masp)
                .Distinct()
                .Count() == cartCount)
            .OrderBy(c => c.tencn)
            .Select(c => new BranchOption { Id = c.macn, Name = c.tencn })
            .ToListAsync();

        var pet = await _db.thucungs
            .AsNoTracking()
            .Where(t => t.makh == makh)
            .OrderBy(t => t.mathucung)
            .Select(t => new { t.mathucung, t.tenthucung })
            .FirstOrDefaultAsync();

        var selectedBranch = string.IsNullOrWhiteSpace(branchId)
            ? branches.FirstOrDefault()?.Id ?? ""
            : branchId;

        return new CheckoutViewModel
        {
            Items = cart,
            Branches = branches,
            SelectedBranch = selectedBranch,
            PaymentMethod = string.IsNullOrWhiteSpace(paymentMethod) ? "Tiền mặt" : paymentMethod,
            PromoCode = promoCode,
            Total = total,
            Discount = discount,
            Payable = total - discount,
            DefaultPetId = pet?.mathucung,
            DefaultPetName = pet?.tenthucung
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
}
