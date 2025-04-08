using System.ComponentModel.DataAnnotations;

namespace Business.Dtos;

public class UpdateStatusDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string StatusName { get; set; } = null!;
}