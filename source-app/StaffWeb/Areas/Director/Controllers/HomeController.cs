using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StaffWeb.Areas.Director.Controllers;

[Area("Director")]
[Authorize(Roles = "Director")]
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
