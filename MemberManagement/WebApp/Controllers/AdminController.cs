using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

public class AdminController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Projects()
    {
        return View();
    }

    public IActionResult Members()
    {
        return View();
    }

    public IActionResult Clients()
    {
        return View();
    }
}
