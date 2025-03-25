using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;


public class ClientsController : Controller
{
    [Route("admin/clients")]
    public IActionResult Index()
    {
        return View();
    }
}
