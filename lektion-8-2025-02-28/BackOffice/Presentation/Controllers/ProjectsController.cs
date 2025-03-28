using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

public class ProjectsController : Controller
{
    [Route("admin/projects")]
    public IActionResult Index()
    {
        return View();
    }
}
