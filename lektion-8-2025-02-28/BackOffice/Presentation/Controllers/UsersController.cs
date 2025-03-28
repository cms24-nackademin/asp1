using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

public class UsersController : Controller
{
    [Route("admin/members")]
    public IActionResult Index()
    {
        return View();
    }
}
