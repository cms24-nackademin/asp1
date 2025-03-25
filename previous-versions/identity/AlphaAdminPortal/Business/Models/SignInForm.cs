using System.ComponentModel.DataAnnotations;

namespace Business.Models;

public class SignInForm
{

    [DataType(DataType.EmailAddress)]
    [Display(Name = "Email", Prompt = "Enter your email address")]
    [Required(ErrorMessage = "Required")]
    public string Email { get; set; } = null!;


    [DataType(DataType.Password)]
    [Display(Name = "Password", Prompt = "Enter your password")]
    [Required(ErrorMessage = "Required")]
    public string Password { get; set; } = null!;

    public bool IsPersistent { get; set; } = false;
}
