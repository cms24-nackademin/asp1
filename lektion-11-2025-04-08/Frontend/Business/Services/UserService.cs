using Business.Dtos;
using Business.Handlers;
using Business.Mappers;
using Business.Models;
using Data.Entities;
using Data.Repositories;
using Microsoft.AspNetCore.Identity;

namespace Business.Services;

public interface IUserService
{
    Task<User?> CreateUserAsync(AddUserDto dto);
    Task<bool> DeleteUserAsync(string id);
    Task<User?> GetUserByEmailAsync(string email);
    Task<User?> GetUserByIdAsync(string id);
    Task<IEnumerable<User>?> GetUsersAsync();
    Task<User?> UpdateUserAsync(UpdateUserDto dto);
}

public class UserService(IUserRepository userRepository, UserManager<UserEntity> userManager, ICacheHandler<IEnumerable<User>> cacheHandler) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly ICacheHandler<IEnumerable<User>> _cacheHandler = cacheHandler;
    private const string _cacheKey = "Users";


    public async Task<User?> CreateUserAsync(AddUserDto dto)
    {
        var exists = await _userRepository.ExistsAsync(x => x.Email == dto.Email);
        if (exists)
            return null!;

        var entity = UserMapper.ToEntity(dto);
        await _userManager.CreateAsync(entity);

        var models = await UpdateCacheAsync();
        return models.FirstOrDefault(x => x.Email == dto.Email);
    }

    public async Task<IEnumerable<User>?> GetUsersAsync()
    {
        var models = _cacheHandler.GetFromCache(_cacheKey) ?? await UpdateCacheAsync();
        return models;
    }

    public async Task<User?> GetUserByIdAsync(string id)
    {
        var cached = _cacheHandler.GetFromCache(_cacheKey);

        var match = cached?.FirstOrDefault(x => x.Id == id);
        if (match != null)
            return match;

        var models = await UpdateCacheAsync();
        return models.FirstOrDefault(x => x.Id == id);
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        var cached = _cacheHandler.GetFromCache(_cacheKey);

        var match = cached?.FirstOrDefault(x => x.Email == email);
        if (match != null)
            return match;

        var models = await UpdateCacheAsync();
        return models.FirstOrDefault(x => x.Email == email);
    }

    public async Task<User?> UpdateUserAsync(UpdateUserDto dto)
    {
        var entity = await _userRepository.GetAsync(x => x.Id == dto.Id);
        if (entity == null)
            return null;

        entity = UserMapper.ToEntity(dto) ?? entity;
        await _userManager.UpdateAsync(entity);

        var models = await UpdateCacheAsync();
        return models.FirstOrDefault(x => x.Id == dto.Id);
    }

    public async Task<bool> DeleteUserAsync(string id)
    {
        var entity = await _userRepository.GetAsync(x => x.Id == id);
        if (entity == null)
            return false;

        await _userManager.DeleteAsync(entity);
        await UpdateCacheAsync();
        return true;
    }

    public async Task<IEnumerable<User>> UpdateCacheAsync()
    {
        var entities = await _userRepository.GetAllAsync();
        var models = entities.Select(UserMapper.ToModel).ToList();

        _cacheHandler.SetCache(_cacheKey, models);
        return models;
    }


}
