using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Business.Dtos;

public class AddClientDto
{
    public IFormFile? ImageFile { get; set; }

    [Required]
    public string ClientName { get; set; } = null!;
}
