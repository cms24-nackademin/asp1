using System.ComponentModel.DataAnnotations;

namespace Business.Dtos;

public class SignInDto
{

    [Required]
    public string Email { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;

    public bool RememberMe { get; set; }

}