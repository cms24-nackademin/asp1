using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Authentication.Models;

public class AppUserProfile
{
    [Key, ForeignKey(nameof(User))]
    public string UserId { get; set; } = null!;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? JobTitle { get; set; }
    public string? PhoneNumber { get; set; }

    public virtual AppUser User { get; set; } = null!;
}
