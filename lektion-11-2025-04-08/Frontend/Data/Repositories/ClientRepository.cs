using Data.Contexts;
using Data.Entities;
using System;

namespace Data.Repositories;

public interface IClientRepository : IBaseRepository<ClientEntity>
{
}

public class ClientRepository(DataContext context) : BaseRepository<ClientEntity>(context), IClientRepository
{
}