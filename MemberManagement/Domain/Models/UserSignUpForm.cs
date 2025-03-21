using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class UserSignUpForm
{
    [Required]
    [DataType(DataType.Text)]
    [Display(Name = "First Name", Prompt = "Enter first name")]
    public string FirstName { get; set; } = null!;

    [Required]
    [DataType(DataType.Text)]
    [Display(Name = "Last Name", Prompt = "Enter last name")]
    public string LastName { get; set; } = null!;

    [Required]
    [DataType(DataType.EmailAddress)]
    [Display(Name = "Email", Prompt = "Enter email address")]
    public string Email { get; set; } = null!;


    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Password", Prompt = "Enter password")]
    public string Password { get; set; } = null!;


    [Compare(nameof(Password))]
    [DataType(DataType.Password)]
    [Display(Name = "Confirm Password", Prompt = "Confirm password")]
    public string ConfirmPassword { get; set; } = null!;
}
