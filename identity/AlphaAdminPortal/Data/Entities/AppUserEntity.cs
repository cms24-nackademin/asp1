using Microsoft.AspNetCore.Identity;

namespace Data.Entities;

public class AppUserEntity : IdentityUser
{
    [ProtectedPersonalData]
    public string? FirstName { get; set; }
    
    [ProtectedPersonalData]
    public string? LastName { get; set; }
    [ProtectedPersonalData]
    public string? ProfileImageUrl { get; set; }
}
