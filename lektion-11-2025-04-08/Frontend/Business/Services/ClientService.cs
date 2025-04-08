using Business.Dtos;
using Business.Handlers;
using Business.Mappers;
using Business.Models;
using Data.Repositories;

namespace Business.Services;

public interface IClientService
{
    Task<Client?> CreateClientAsync(AddClientDto dto);
    Task<bool> DeleteClientAsync(string id);
    Task<Client?> GetClientByIdAsync(string id);
    Task<IEnumerable<Client>?> GetClientsAsync();
    Task<Client?> UpdateClientAsync(UpdateClientDto dto);
}

public class ClientService(IClientRepository clientRepository, ICacheHandler<IEnumerable<Client>> cacheHandler) : IClientService
{
    private readonly IClientRepository _clientRepository = clientRepository;
    private readonly ICacheHandler<IEnumerable<Client>> _cacheHandler = cacheHandler;
    private const string _cacheKey = "Clients";

    public async Task<Client?> CreateClientAsync(AddClientDto dto)
    {
        var entity = ClientMapper.ToEntity(dto);
        await _clientRepository.AddAsync(entity);

        var models = await UpdateCacheAsync();
        return models.FirstOrDefault(x => x.ClientName == dto.ClientName);
    }

    public async Task<IEnumerable<Client>?> GetClientsAsync()
    {
        var models = _cacheHandler.GetFromCache(_cacheKey) ?? await UpdateCacheAsync();
        return models;
    }

    public async Task<Client?> GetClientByIdAsync(string id)
    {
        var cached = _cacheHandler.GetFromCache(_cacheKey);

        var match = cached?.FirstOrDefault(x => x.Id == id);
        if (match != null)
            return match;

        var models = await UpdateCacheAsync();
        return models.FirstOrDefault(x => x.Id == id);
    }

    public async Task<Client?> UpdateClientAsync(UpdateClientDto dto)
    {
        var entity = await _clientRepository.GetAsync(x => x.Id == dto.Id);
        if (entity == null)
            return null;

        entity = ClientMapper.ToEntity(dto) ?? entity;
        await _clientRepository.UpdateAsync(entity);

        var models = await UpdateCacheAsync();
        return models.FirstOrDefault(x => x.Id == dto.Id);
    }

    public async Task<bool> DeleteClientAsync(string id)
    {
        var entity = await _clientRepository.GetAsync(x => x.Id == id);
        if (entity == null)
            return false;

        await _clientRepository.DeleteAsync(x => x.Id == id);
        await UpdateCacheAsync();
        return true;
    }

    public async Task<IEnumerable<Client>> UpdateCacheAsync()
    {
        var entities = await _clientRepository.GetAllAsync();
        var models = entities.Select(ClientMapper.ToModel).ToList();

        _cacheHandler.SetCache(_cacheKey, models);
        return models;
    }
}
