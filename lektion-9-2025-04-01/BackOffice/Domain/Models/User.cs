namespace Domain.Models;

public class User
{
    public string Id { set; get; } = null!;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? JobTitle { get; set; }
    public string Email { get; set; } = null!;
    public string? PhoneNumber { get; set; }
    public string? Image { get; set; }
}
