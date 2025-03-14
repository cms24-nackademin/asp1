using System.ComponentModel.DataAnnotations;

namespace Business.Models;

public class AddClientForm
{
    [DataType(DataType.Text)]   
    [Display(Name = "Client Name", Prompt = "Enter client name")]
    [Required(ErrorMessage = "Required")]
    public string ClientName { get; set; } = null!;

    [DataType(DataType.EmailAddress)]
    [Display(Name = "Email", Prompt = "Enter email address")]
    [Required(ErrorMessage = "Required")]
    [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Invalid email")]
    public string Email { get; set; } = null!;


    [DataType(DataType.PhoneNumber)]
    [Display(Name = "Phone", Prompt = "Enter phone number (optional)")]
    public string? Phone { get; set; }


    [DataType(DataType.Text)]
    [Display(Name = "Location", Prompt = "Enter location (optional)")]
    public string? Location { get; set; }
}
