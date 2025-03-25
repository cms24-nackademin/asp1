using Authentication.Models;
using Microsoft.AspNetCore.Identity;

namespace Authentication.Services;

public interface IAuthService
{
    Task<bool> LoginAsync(string email, string password, bool rememberMe = false);
    Task LogoutAsync();
}

public class AuthService(SignInManager<AppUser> signInManager) : IAuthService
{
    private readonly SignInManager<AppUser> _signInManager = signInManager;

    public async Task<bool> LoginAsync(string email, string password, bool rememberMe = false)
    {
        var result = await _signInManager.PasswordSignInAsync(email, password, rememberMe, false);
        return result.Succeeded;
    }

    public async Task LogoutAsync()
    {
        await _signInManager.SignOutAsync();
    }
}
