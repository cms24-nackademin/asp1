using Data.Contexts;
using Data.Entitites;

namespace Data.Repositories;

public interface IStatusRepository : IBaseRepository<StatusEntity>
{

}

public class StatusRepository(AppDbContext context) : BaseRepository<StatusEntity>(context), IStatusRepository
{
}
