using Data.Entities;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Business.Services;

public interface IAuthService
{
    Task<bool> AlreadyExistsAsync(string email);
    Task<bool> LoginAsync(UserLoginForm loginForm);
}

public class AuthService(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager) : IAuthService
{
    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly SignInManager<UserEntity> _signInManager = signInManager;

    public async Task<bool> LoginAsync(UserLoginForm loginForm)
    {
        var result = await _signInManager.PasswordSignInAsync(loginForm.Email, loginForm.Password, loginForm.IsPersistent, false);
        return result.Succeeded;
    }


    public async Task<bool> AlreadyExistsAsync(string email)
    {
        var result = await _userManager.Users.AnyAsync(x => x.Email == email);
        return result;
    }

    public async Task<bool> SignUpAsync(UserSignUpForm signUpForm)
    {
        var userEntity = new UserEntity()
        {
            UserName = signUpForm.Email,
            FirstName = signUpForm.FirstName,
            LastName = signUpForm.LastName,
            Email = signUpForm.Email
        };

        var result = await _userManager.CreateAsync(userEntity, signUpForm.Password);
        if (result.Succeeded)
        {
            await _signInManager.PasswordSignInAsync(signUpForm.Email, signUpForm.Password, false, false);
        }

        return result.Succeeded;
    }
}
