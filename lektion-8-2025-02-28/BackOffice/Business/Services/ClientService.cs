using Data.Repositories;
using Domain.Models;
using Domain.Responses;

namespace Business.Services;

public class ClientService(IClientRepository clientRepository)
{
    private readonly IClientRepository _clientRepository = clientRepository;

    public async Task<ClientResult<IEnumerable<Client>> GetClientsAsync()
    {
        var repositoryResult = await _clientRepository.GetAllAsync
            (
                orderByDescending: false,
                sortByColumn: x => x.ClientName
            );

        var entities = repositoryResult.Result;
        return entities;
    }
}
