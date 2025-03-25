using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[Authorize]
public class ProjectsController : Controller
{
    [Route("admin/projects")]
    public IActionResult Index()
    {
        return View();
    }
}
