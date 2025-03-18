using Business.Models;
using Business.Services;
using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApp.Controllers;

public class AuthController(IUserService userService, SignInManager<AppUserEntity> signInManager) : Controller
{
    private readonly IUserService _userService = userService;
    private readonly SignInManager<AppUserEntity> _signInManager = signInManager;

    public IActionResult SignUp()
    {
        ViewBag.ErrorMessage = "";
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> SignUp(SignUpForm form)
    {
        ViewBag.ErrorMessage = "";

        if (!ModelState.IsValid)
            return View(form);

        if (await _userService.UserAlreadyExists(form.Email))
        {
            ViewBag.ErrorMessage = "User already exists.";
            return View(form);
        }

        var result = await _userService.CreateUserAsync(form);
        if (result)
            return RedirectToAction("SignIn");

        ViewBag.ErrorMessage = "Unable to register user account right now!";
        return View(form);
    }







    public IActionResult SignIn()
    {
        ViewBag.ErrorMessage = "";
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> SignIn(SignInForm form)
    {
        ViewBag.ErrorMessage = "";

        if (!ModelState.IsValid)
        {
            ViewBag.ErrorMessage = "Invalid email or password";
            return View(form);
        }

        var result = await _signInManager.PasswordSignInAsync(form.Email, form.Password, form.IsPersistent, false);
        if (result.Succeeded)
            return RedirectToAction("Clients", "Admin");

        ViewBag.ErrorMessage = "Invalid email or password";
        return View(form);
    }
}
