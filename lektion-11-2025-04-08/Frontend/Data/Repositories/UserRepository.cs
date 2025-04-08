using Data.Contexts;
using Data.Entities;
using System;

namespace Data.Repositories;

public interface IUserRepository : IBaseRepository<UserEntity>
{
}

public class UserRepository(DataContext context) : BaseRepository<UserEntity>(context), IUserRepository
{
}
