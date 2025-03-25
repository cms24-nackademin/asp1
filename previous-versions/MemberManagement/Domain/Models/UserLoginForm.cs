using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class UserLoginForm
{
    [Required]
    [DataType(DataType.EmailAddress)]
    [Display(Name = "Email", Prompt = "Enter email address")]
    public string Email { get; set; } = null!;


    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Password", Prompt = "Enter password")]
    public string Password { get; set; } = null!;


    [Display(Name = "Remember me")]
    public bool IsPersistent { get; set; }

}
