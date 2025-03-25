using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[Authorize(Roles = "Admin")]
public class MembersController : Controller
{
    [Route("admin/members")]
    public IActionResult Index()
    {
        return View();
    }
}
