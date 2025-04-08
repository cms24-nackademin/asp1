using Business.Dtos;
using Business.Services;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers;

public class AuthController(IAuthService authService) : Controller
{
    private readonly IAuthService _authService = authService;

    [Route("auth/signup")]
    public IActionResult SignUp()
    {
        return View();
    }

    [HttpPost]
    [Route("auth/signup")]
    public async Task<IActionResult> SignUp(SignUpViewModel  model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var signUpDto = new SignUpDto
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            Password = model.Password,
        };

        var result = await _authService.SignUpAsync(signUpDto);
        if (result)
            return LocalRedirect("~/");

        ViewBag.ErrorMessage = "Unable to create user account.";
        return View();
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
        ViewBag.ReturnUrl = returnUrl;
        ViewBag.ErrorMessage = "";

        if (!ModelState.IsValid)
            return View(model);

        var signInDto = new SignInDto
        {
            Email = model.Email,
            Password = model.Password,
            RememberMe = model.RememberMe,
        };

        var result = await _authService.SignInAsync(signInDto);
        if (result)
            return LocalRedirect(returnUrl);


        ViewBag.ErrorMessage = "Invalid email or password";
        return View();
    }
}
