using System.ComponentModel.DataAnnotations;

namespace Business.Dtos;

public class AddStatusDto
{
    [Required]
    public string StatusName { get; set; } = null!;
}