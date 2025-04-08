using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Mappers;

public static class StatusMapper
{
    public static StatusEntity ToEntity(AddStatusDto? dto)
    {
        if (dto == null) return null!;
        return new StatusEntity
        {
            StatusName = dto.StatusName
        };
    }

    public static StatusEntity ToEntity(UpdateStatusDto? dto)
    {
        if (dto == null) return null!;
        return new StatusEntity
        {
            Id = dto.Id,
            StatusName = dto.StatusName
        };
    }

    public static Status ToModel(StatusEntity? entity)
    {
        if (entity == null) return null!;
        return new Status
        {
            Id = entity.Id,
            StatusName = entity.StatusName
        };
    }
}
