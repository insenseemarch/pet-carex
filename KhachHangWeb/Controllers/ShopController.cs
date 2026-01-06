using KhachHangWeb.Data;
using KhachHangWeb.Extensions;
using KhachHangWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class ShopController : Controller
{
    private readonly AppDbContext _db;
    public ShopController(AppDbContext db) => _db = db;

    public async Task<IActionResult> Index(string? q, string? loai, string? gia)
    {
        var query = _db.sanphams.AsQueryable();

        if (!string.IsNullOrWhiteSpace(q))
            query = query.Where(p => p.tensp.Contains(q) || p.loaisp.Contains(q));

        if (!string.IsNullOrWhiteSpace(loai) && loai != "all")
            query = query.Where(p => p.loaisp == loai);

        if (!string.IsNullOrWhiteSpace(gia) && gia != "all")
        {
            query = gia switch
            {
                "lt100" => query.Where(p => p.gia < 100_000),
                "100-300" => query.Where(p => p.gia >= 100_000 && p.gia <= 300_000),
                "300-500" => query.Where(p => p.gia > 300_000 && p.gia <= 500_000),
                "gt500" => query.Where(p => p.gia > 500_000),
                _ => query
            };
        }

        ViewBag.Categories = await _db.sanphams
            .Select(p => p.loaisp)
            .Distinct()
            .OrderBy(x => x)
            .ToListAsync();

        var products = await query.Take(50).ToListAsync();
        ViewBag.SelectedCategory = loai ?? "all";
        ViewBag.Keyword = q ?? "";
        ViewBag.SelectedPrice = gia ?? "all";
        return View(products);
    }

    [HttpPost]
    public async Task<IActionResult> AddToCart(string id, int qty = 1)
    {
        var product = await _db.sanphams.FindAsync(id);
        if (product == null) return NotFound();
        var cart = HttpContext.Session.GetObject<List<CartItem>>("CART") ?? new();
        var item = cart.FirstOrDefault(x => x.MaSP == id);
        if (item == null)
            cart.Add(new CartItem { MaSP = id, Quantity = qty, UnitPrice = product.gia, Name = product.tensp });
        else
            item.Quantity += qty;
        HttpContext.Session.SetObject("CART", cart);
        return RedirectToAction(nameof(Index));
    }

}
