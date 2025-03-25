using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

public class MembersController : Controller
{
    [Route("admin/members")]
    public IActionResult Index()
    {
        return View();
    }
}
