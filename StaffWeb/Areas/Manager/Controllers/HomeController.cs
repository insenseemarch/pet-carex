using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StaffWeb.Areas.Manager.Controllers;

[Area("Manager")]
[Authorize(Roles = "Manager")]
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
