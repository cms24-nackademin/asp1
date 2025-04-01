using Data.Contexts;
using Data.Entitites;

namespace Data.Repositories;

public interface IUserRepository : IBaseRepository<UserEntity>
{

}

public class UserRepository(AppDbContext context) : BaseRepository<UserEntity>(context), IUserRepository
{
}
