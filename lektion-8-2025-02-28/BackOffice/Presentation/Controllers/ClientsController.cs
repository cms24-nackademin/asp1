using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

public class ClientsController : Controller
{
    [Route("admin/clients")]
    public IActionResult Index()
    {
        return View();
    }
}
