using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Mappers;

public static class UserMapper
{
    public static UserEntity ToEntity(SignUpDto? dto)
    {
        if (dto == null) return null!;
        return new UserEntity
        {
            UserName = dto.Email,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email
        };
    }

    public static UserEntity ToEntity(AddUserDto? dto, string? newImageFileName = null)
    {
        if (dto == null) return null!;
        return new UserEntity
        {
            UserName = dto.Email,
            ImageFileName = newImageFileName,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email
        };
    }

    public static UserEntity ToEntity(UpdateUserDto? dto, string? newImageFileName = null)
    {
        if (dto == null) return null!;
        return new UserEntity
        {
            Id = dto.Id,
            UserName = dto.Email,
            ImageFileName = newImageFileName ?? dto.ImageFileName,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,
            StreetName = dto.StreetName,
            PostalCode = dto.PostalCode,
            City = dto.City
        };
    }

    public static User ToModel(UserEntity? entity)
    {
        if (entity == null) return null!;
        return new User
        {
            Id = entity.Id,
            ImageFileName = entity.ImageFileName,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Email = entity.Email!,
            PhoneNumber = entity.PhoneNumber,
            StreetName = entity.StreetName,
            PostalCode = entity.PostalCode,
            City = entity.City
        };
    }
}
