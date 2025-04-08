using Business.Dtos;
using Business.Handlers;
using Business.Mappers;
using Business.Models;
using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Business.Services;

public interface IAuthService
{
    Task<bool> SignUpAsync(SignUpDto dto);
    Task<bool> SignInAsync(SignInDto dto);
}

public class AuthService(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager, ICacheHandler<IEnumerable<User>> cacheHandler) : IAuthService
{
    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly SignInManager<UserEntity> _signInManager = signInManager;
    private readonly ICacheHandler<IEnumerable<User>> _cacheHandler = cacheHandler;
    private const string _cacheKey = "Users";


    public async Task<bool> SignInAsync(SignInDto dto)
    {
        var exists = await _userManager.Users.AnyAsync(x => x.Email == dto.Email);
        if (!exists)
            return false;

        var result = await _signInManager.PasswordSignInAsync(dto.Email, dto.Password, dto.RememberMe, false);
        return result.Succeeded;
    }

    public async Task<bool> SignUpAsync(SignUpDto dto)
    {
        var exists = await _userManager.Users.AnyAsync(x => x.Email == dto.Email);
        if (exists)
            return false;

        var entity = UserMapper.ToEntity(dto);
        var result = await _userManager.CreateAsync(entity, dto.Password);

        var models = await UpdateCacheAsync();
        return result.Succeeded;
    }


    public async Task<IEnumerable<User>> UpdateCacheAsync()
    {
        var entities = await _userManager.Users.ToListAsync();
        var models = entities.Select(UserMapper.ToModel).ToList();

        _cacheHandler.SetCache(_cacheKey, models);
        return models;
    }


}
