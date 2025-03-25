using Business.Models;

namespace Business.Services;

public class ClientService
{
    public async Task<int> CreateAsync(AddClientForm form)
    {
        try
        {
            if (await _clientRepository.ExistsAsync(x => x.ClientName == form.ClientName))
                return 409;

            return 200;
        }
        catch
        {
            return 500;
        }
    }
}
