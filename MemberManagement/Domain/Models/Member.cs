using System.Data.Common;

namespace Domain.Models;

public class Member
{
    public string? Id { get; set; }
    public string? ImageUrl { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? JobTitle { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public MemberAddress Address { get; set; } = new();
}
