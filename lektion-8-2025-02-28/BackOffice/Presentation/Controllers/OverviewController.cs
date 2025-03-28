using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

public class OverviewController : Controller
{
    [Route("admin/overview")]
    public IActionResult Index()
    {
        return View();
    }
}
