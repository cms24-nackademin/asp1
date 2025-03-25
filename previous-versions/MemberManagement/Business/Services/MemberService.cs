using Data.Entities;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Business.Services;

public interface IMemberService
{
    Task<IEnumerable<Member>> GetMembersAsync();
}

public class MemberService(UserManager<UserEntity> userManager) : IMemberService
{
    private readonly UserManager<UserEntity> _userManager = userManager;


    public async Task<IEnumerable<Member>> GetMembersAsync()
    {
        var list = await _userManager.Users.Include(x => x.Address).ToListAsync();
        var members = list.Select(x => new Member
        {
            Id = x.Id,
            ImageUrl = x.ImageUrl,
            FirstName = x.FirstName,
            LastName = x.LastName,
            JobTitle = x.JobTitle,
            Email = x.Email,
            PhoneNumber = x.PhoneNumber,
            Address = new MemberAddress
            {
                StreetName = x.Address?.StreetName,
                PostalCode = x.Address?.PostalCode,
                City = x.Address?.City,
            }
        });

        return members;
    }
}
