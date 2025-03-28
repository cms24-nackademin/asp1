using Business.Models;
using Data.Entitites;
using Domain.Extensions;
using Domain.Responses;
using Microsoft.AspNetCore.Identity;

namespace Business.Services;

public interface IAuthService
{
    Task<AuthResult> SignInAsync(SignInFormData formData);
    Task SignOutAsync();
    Task<AuthResult> SignUpAsync(SignUpFormData formData);
}

public class AuthService(SignInManager<UserEntity> signInManager, UserManager<UserEntity> userManager, IUserService userService) : IAuthService
{
    private readonly SignInManager<UserEntity> _signInManager = signInManager;
    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly IUserService _userService = userService;


    public async Task<AuthResult> SignUpAsync(SignUpFormData formData)
    {
        if (formData == null)
            return new AuthResult { Succeeded = false, StatusCode = 400, Error = "form data can't be null." };

        var userResult = await _userService.UserExistsByEmailAsync(formData.Email);
        if (userResult.Succeeded)
            return new AuthResult { Succeeded = false, StatusCode = 409, Error = userResult.Error };

        try
        {
            var userEntity = formData.MapTo<UserEntity>();
            userEntity.UserName = userEntity.Email;

            var identityResult = await _userManager.CreateAsync(userEntity, formData.Password);
            if (identityResult.Succeeded)
            {
                if (formData.RoleName != null)
                {
                    var result = await _userService.AddUserToRoleAsync(userEntity, formData.RoleName);
                }

                return new AuthResult { Succeeded = true, StatusCode = 201, SuccessMessage = $"User was created successfully." };
            }

            throw new Exception("Unable to sign up user");

        }
        catch (Exception ex)
        {
            return new AuthResult { Succeeded = false, StatusCode = 500, Error = ex.Message };
        }
    }

    public async Task<AuthResult> SignInAsync(SignInFormData formData)
    {
        if (formData == null)
            return new AuthResult { Succeeded = false, StatusCode = 400, Error = "form data can't be null." };

        var signInResult = await _signInManager.PasswordSignInAsync(formData.Email, formData.Password, formData.IsPersistent, false);
        return signInResult.Succeeded
            ? new AuthResult { Succeeded = true, StatusCode = 200 }
            : new AuthResult { Succeeded = false, StatusCode = 401 };
    }

    public async Task SignOutAsync()
    {
        await _signInManager.SignOutAsync();
    }
}
