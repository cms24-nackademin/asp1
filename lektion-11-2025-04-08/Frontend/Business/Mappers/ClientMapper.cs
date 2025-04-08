using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Mappers;

public static class ClientMapper
{
    public static ClientEntity ToEntity(AddClientDto? dto, string? newImageFileName = null)
    {
        if (dto == null) return null!;
        return new ClientEntity
        {
            ImageFileName = newImageFileName,
            ClientName = dto.ClientName
        };
    }

    public static ClientEntity ToEntity(UpdateClientDto? dto, string? newImageFileName = null)
    {
        if (dto == null) return null!;
        return new ClientEntity
        {
            Id = dto.Id,
            ImageFileName = newImageFileName ?? dto.ImageFileName,
            ClientName = dto.ClientName
        };
    }

    public static Client ToModel(ClientEntity? entity)
    {
        if (entity == null) return null!;
        return new Client
        {
            Id = entity.Id,
            ImageFileName = entity.ImageFileName,
            ClientName = entity.ClientName
        };
    }
}
