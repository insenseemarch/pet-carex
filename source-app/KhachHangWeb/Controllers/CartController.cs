using KhachHangWeb.Extensions;
using KhachHangWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace KhachHangWeb.Controllers;

public class CartController : Controller
{
    public IActionResult Index()
    {
        var cart = HttpContext.Session.GetObject<List<CartItem>>("CART") ?? new();
        return View(cart);
    }

    [HttpPost]
    public IActionResult Update(string id, int qty)
    {
        var cart = HttpContext.Session.GetObject<List<CartItem>>("CART") ?? new();
        var item = cart.FirstOrDefault(x => x.MaSP == id);
        if (item != null) item.Quantity = Math.Max(1, qty);
        HttpContext.Session.SetObject("CART", cart);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public IActionResult Remove(string id)
    {
        var cart = HttpContext.Session.GetObject<List<CartItem>>("CART") ?? new();
        cart.RemoveAll(x => x.MaSP == id);
        HttpContext.Session.SetObject("CART", cart);
        return RedirectToAction(nameof(Index));
    }
}
