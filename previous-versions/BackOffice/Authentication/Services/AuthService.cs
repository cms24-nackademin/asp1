using Authentication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Authentication.Services;

public interface IAuthService
{
    Task<bool> LoginAsync(string email, string password, bool rememberMe = false);
    Task LogoutAsync();
    Task<bool> UserExistsAsync(string email);
    Task<IdentityResult> SignUpAsync(SignUpDto dto, string role = "User");
}

public class AuthService(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager) : IAuthService
{
    private readonly SignInManager<AppUser> _signInManager = signInManager;
    private readonly UserManager<AppUser> _userManager = userManager;

    public async Task<bool> LoginAsync(string email, string password, bool rememberMe = false)
    {
        var result = await _signInManager.PasswordSignInAsync(email, password, rememberMe, false);
        return result.Succeeded;
    }

    public async Task LogoutAsync()
    {
        await _signInManager.SignOutAsync();
    }

    public async Task<IdentityResult> SignUpAsync(SignUpDto dto, string roleName = "User")
    {
        if (dto == null)
            return null!;

        var adminUser = new AppUser
        {
            UserName = dto.Email,
            Email = dto.Email
        };

        adminUser.Profile = new AppUserProfile
        {
            UserId = adminUser.Id,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
        };

        adminUser.Address = new AppUserAddress
        {
            UserId = adminUser.Id
        };

        var result = await _userManager.CreateAsync(adminUser, dto.Password);
        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(adminUser, roleName);
        }

        return result;
    }

    public async Task<bool> UserExistsAsync(string email)
    {
        return await _userManager.Users.AnyAsync(x => x.Email == email);
    }
}
