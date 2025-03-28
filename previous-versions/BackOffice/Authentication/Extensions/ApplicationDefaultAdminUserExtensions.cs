using Authentication.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Data;

namespace Authentication.Extensions;

public static class ApplicationDefaultAdminUserExtensions
{
    public static IApplicationBuilder UseDefaultAdminAccount(this IApplicationBuilder app, string email = "admin@domain.com", string password = "BytMig123!", string firstName = "System", string lastName = "Administrator", string role = "Admin")
    {
        return app.UseMiddleware<DefaultAdminAccountMiddleware>(email, password, firstName, lastName, role);
    }


    public class DefaultAdminAccountMiddleware(RequestDelegate next, string email, string password, string firstName, string lastName, string role)
    {
        private readonly RequestDelegate _next = next;
        private readonly string _email = email;
        private readonly string _password = password;
        private readonly string _firstName = firstName;
        private readonly string _lastName = lastName;
        private readonly string _role = role;

        public async Task InvokeAsync(HttpContext context, UserManager<AppUser> userManager)
        {
            var adminUser = await userManager.FindByEmailAsync(_email);
            if (adminUser == null)
            {
                adminUser = new AppUser 
                { 
                    UserName = _email, 
                    Email = _email 
                };
                
                adminUser.Profile = new AppUserProfile 
                { 
                    UserId = adminUser.Id, 
                    FirstName = _firstName, 
                    LastName = _lastName,
                };
                
                adminUser.Address = new AppUserAddress 
                {  
                    UserId = adminUser.Id 
                };

                var result = await userManager.CreateAsync(adminUser, _password);
                if (result.Succeeded)
                {
                    if (_role != null)
                        await userManager.AddToRoleAsync(adminUser, _role);
                }
            }

            await _next(context);
        }
    }
}
