using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StaffWeb.Areas.Staff.Controllers;

[Area("Staff")]
[Authorize(Roles = "Staff")]
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
