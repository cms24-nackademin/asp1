using Business.Models;
using Business.Services;
using Domain.Extensions;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;
using System.Threading.Tasks;

namespace Presentation.Controllers;

public class AuthController(IAuthService authService) : Controller
{
    private readonly IAuthService _authService = authService;


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

        var signUpFormData = model.MapTo<SignUpFormData>();
        var authResult = await _authService.SignUpAsync(signUpFormData);

        if (authResult.Succeeded)
        {
            return LocalRedirect(returnUrl);
        }

        ViewBag.ReturnUrl = returnUrl;
        ViewBag.ErrorMessage = authResult.Error;
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
            var signInFormData = model.MapTo<SignInFormData>(); 
            var authResult = await _authService.SignInAsync(signInFormData);

            if (authResult.Succeeded)
            {
                return LocalRedirect(returnUrl);
            }
        }

        ViewBag.ReturnUrl = returnUrl;
        ViewBag.ErrorMessage = "Unable to login. Try another email or password.";
        return View(model);
    }

    [Route("auth/logout")]
    public async Task<IActionResult> Logout()
    {
        await _authService.SignOutAsync();
        return LocalRedirect("~/");
    }
}
