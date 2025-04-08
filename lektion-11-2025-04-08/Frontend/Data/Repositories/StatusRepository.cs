using Data.Contexts;
using Data.Entities;
using System;

namespace Data.Repositories;

public interface IStatusRepository : IBaseRepository<StatusEntity>
{
}

public class StatusRepository(DataContext context) : BaseRepository<StatusEntity>(context), IStatusRepository
{
}
