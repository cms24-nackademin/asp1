using Business.Models;
using Data.Entitites;
using Data.Repositories;
using Domain.Extensions;
using Domain.Models;
using Domain.Responses;
using Microsoft.AspNetCore.Identity;

namespace Business.Services;

public class UserService(IUserRepository userRepository, UserManager<UserEntity> userManager, RoleManager<IdentityRole> roleManager)
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly RoleManager<IdentityRole> _roleManager = roleManager;


    public async Task<UserResult<IEnumerable<User>>> GetUsersAsync()
    {
        var repositoryResult = await _userRepository.GetAllAsync
            (
                orderByDescending: false,
                sortByColumn: x => x.FirstName!
            );

        var entities = repositoryResult.Result;
        var users = entities?.Select(entity => entity.MapTo<User>()) ?? [];

        return new UserResult<IEnumerable<User>> { Succeeded = true, StatusCode = 200, Result = users };
    }
    public async Task<UserResult<User>> GetUserByIdAsync(string id)
    {
        var repositoryResult = await _userRepository.GetAsync(x => x.Id == id);

        var entity = repositoryResult.Result;
        if (entity == null)
            return new UserResult<User> { Succeeded = false, StatusCode = 404, Error = $"User with id '{id}' was not found." };

        var user = entity.MapTo<User>();
        return new UserResult<User> { Succeeded = true, StatusCode = 200, Result = user };
    }

    public async Task<UserResult> CreateUserAsync(CreateUserFormData formData)
    {
        if (formData == null)
            return new UserResult { Succeeded = false, StatusCode = 400, Error = "form data can't be null." };

        var existsResult = await _userRepository.ExistsAsync(x => x.Email == formData.Email);
        if (existsResult.Succeeded)
            return new UserResult { Succeeded = false, StatusCode = 409, Error = "A user with the same email address already exists." };
    
        try
        {
            var userEntity = formData.MapTo<UserEntity>();

            var identityResult = await _userManager.CreateAsync(userEntity, formData.Password);
            if (identityResult.Succeeded)
            {
                if (formData.RoleName != null)
                {
                    if(await _roleManager.RoleExistsAsync(formData.RoleName))
                    {
                        await _userManager.AddToRoleAsync(userEntity, formData.RoleName);
                        return new UserResult { Succeeded = true, StatusCode = 201, SuccessMessage = $"User was created successfully and added to '{formData.RoleName}'." };
                    }
                }

                return new UserResult { Succeeded = true, StatusCode = 201, SuccessMessage = $"User was created successfully." };
            }

            throw new Exception("Unable to create user");

        }
        catch (Exception ex)
        {
            return new UserResult { Succeeded = false, StatusCode = 500, Error = ex.Message };
        }
    } 
}
