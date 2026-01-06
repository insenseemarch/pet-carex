using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KhachHangWeb.Controllers;

[Authorize]
public class LookupController : Controller
{
    public IActionResult Index() => View();

    [HttpPost]
    public IActionResult Order(string orderId) => NotFound();

    [HttpPost]
    public IActionResult Booking(string bookingId) => NotFound();
}
