using Data.Contexts;
using Data.Entitites;

namespace Data.Repositories;

public interface IClientRepository : IBaseRepository<ClientEntity>
{

}

public class ClientRepository(AppDbContext context) : BaseRepository<ClientEntity>(context), IClientRepository
{
}
