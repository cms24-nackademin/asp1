using Microsoft.AspNetCore.Mvc;
using Presentation.Models;

namespace Presentation.Controllers;

public class AuthController : Controller
{
    [Route("auth/signup")]
    public IActionResult SignUp(string returnUrl = "~/")
    {
        ViewBag.ReturnUrl = returnUrl;
        ViewBag.ErrorMessage = "";

        return View();
    }

    [HttpPost]
    [Route("auth/signup")]
    public async Task<IActionResult> SignUp(SignUpViewModel model, string returnUrl = "~/")
    {
        if (!ModelState.IsValid)
        {
            ViewBag.ReturnUrl = returnUrl;
            ViewBag.ErrorMessage = "";
            return View(model);
        }

        ViewBag.ReturnUrl = returnUrl;
        ViewBag.ErrorMessage = "Something went wrong. Try again later.";
        return View(model);
    }


    [Route("auth/login")]
    public IActionResult Login(string returnUrl = "~/")
    {
        ViewBag.ReturnUrl = returnUrl;
        ViewBag.ErrorMessage = "";

        return View();
    }

    [HttpPost]
    [Route("auth/login")]
    public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = "~/")
    {
        if (ModelState.IsValid)
        {

        }

        ViewBag.ReturnUrl = returnUrl;
        ViewBag.ErrorMessage = "Unable to login. Try another email or password.";
        return View(model);
    }

    [Route("auth/logout")]
    public IActionResult Logout()
    {
        return LocalRedirect("~/");
    }
}
