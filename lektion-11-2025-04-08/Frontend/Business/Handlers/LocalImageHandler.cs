using Azure.Storage.Blobs.Specialized;
using Microsoft.AspNetCore.Http;

namespace Business.Handlers;

public class LocalImageHandler(string imagePath) : IImageHandler
{
    private readonly string _imagePath = imagePath;

    public async Task<string?> SaveProjectImageAsync(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return null!;

        var extension = Path.GetExtension(file.FileName);
        var fileName = $"{Guid.NewGuid()}{extension}";

        if (!Directory.Exists(_imagePath))
            Directory.CreateDirectory(_imagePath);

        var filePath = Path.Combine(_imagePath, fileName);
        using var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream);

        return fileName;
    }
}
