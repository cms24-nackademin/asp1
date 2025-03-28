using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[Authorize]
public class OverviewController : Controller
{
    [Route("admin/overview")]
    public IActionResult Index()
    {
        return View();
    }
}
