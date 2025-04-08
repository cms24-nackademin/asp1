using Microsoft.AspNetCore.Identity;

namespace Data.Entities;

public class UserEntity : IdentityUser
{
    [ProtectedPersonalData]
    public string? ImageFileName { get; set; }

    [ProtectedPersonalData]
    public string? FirstName { get; set; }

    [ProtectedPersonalData]
    public string? LastName { get; set; }

    [ProtectedPersonalData]
    public string? JobTitle { get; set; }

    [ProtectedPersonalData]
    public string? StreetName { get; set; }

    [ProtectedPersonalData]
    public string? PostalCode { get; set; }

    [ProtectedPersonalData]
    public string? City { get; set; }

    public virtual ICollection<ProjectEntity> Projects { get; set; } = [];
}