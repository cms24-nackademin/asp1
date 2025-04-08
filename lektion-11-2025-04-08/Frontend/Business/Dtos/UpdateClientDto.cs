using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Business.Dtos;

public class UpdateClientDto
{
    [Required]
    public string Id { get; set; } = null!;

    public string? ImageFileName { get; set; }
    public IFormFile? NewImageFile { get; set; }

    [Required]
    public string ClientName { get; set; } = null!;
}