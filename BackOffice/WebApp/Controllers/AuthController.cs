using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

public class AuthController : Controller
{

    [Route("auth/signup")]
    public IActionResult SignUp()
    {
        return View();
    }

    [Route("auth/login")]
    public IActionResult Login()
    {
        return View();
    }


    [Route("auth/logout")]
    public IActionResult Logout()
    {
        return LocalRedirect("~/");
    }
}
