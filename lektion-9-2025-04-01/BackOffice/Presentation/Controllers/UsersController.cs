using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Authorize(Roles = "Admin")]
public class UsersController : Controller
{
    [Route("admin/members")]
    public IActionResult Index()
    {
        return View();
    }
}
