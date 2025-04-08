using Microsoft.AspNetCore.Http;

namespace Business.Handlers;

public interface IImageHandler
{
    Task<string?> SaveProjectImageAsync(IFormFile file);
}
