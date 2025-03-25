using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

public class ProjectsController : Controller
{
    [Route("admin/projects")]
    public IActionResult Index()
    {
        return View();
    }
}
