using Business.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

public class AuthController(IAuthService authService) : Controller
{
    private readonly IAuthService _authService = authService;

    public IActionResult Login(string returnUrl = "~/")
    {
        ViewBag.ErrorMessage = null;
        ViewBag.ReturnUrl = returnUrl;

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(UserLoginForm userLoginForm, string returnUrl = "~/")
    {
        if (ModelState.IsValid)
        {
            var result = await _authService.LoginAsync(userLoginForm);
            if (result)
                return LocalRedirect(returnUrl);
        }

        ViewBag.ErrorMessage = "No user account found, try a different email or password.";
        return View(userLoginForm);
    }

    public IActionResult SignUp(string returnUrl = "~/")
    {
        ViewBag.ErrorMessage = null;
        ViewBag.ReturnUrl = returnUrl;

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> SignUp(UserSignUpForm userSignUpForm, string returnUrl = "~/")
    {
        if (ModelState.IsValid)
        {
            if (await _authService.AlreadyExistsAsync(userSignUpForm.Email))
            {
                ViewBag.ErrorMessage = "A user with the same email already exists.";
                return View(userSignUpForm);
            }

            if (await _authService.SignUpAsync(userSignUpForm))
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                ViewBag.ErrorMessage = "Something went wrong. Try again later.";
            }
        }

        return View(userSignUpForm);
    }
}
