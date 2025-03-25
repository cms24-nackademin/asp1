using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;


[Authorize (Roles = "Admin")]
public class ClientsController : Controller
{
    [Route("admin/clients")]
    public IActionResult Index()
    {
        return View();
    }
}
