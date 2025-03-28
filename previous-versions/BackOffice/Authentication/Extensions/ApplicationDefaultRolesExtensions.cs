using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Authentication.Extensions;

public static class ApplicationDefaultRolesExtensions
{
    public static IApplicationBuilder UseDefaultRoles(this IApplicationBuilder app, IEnumerable<string>? roles = null)
    {
        roles ??= ["Admin", "User" ];
        return app.UseMiddleware<DefaultRolesMiddleware>(roles);
    }
}

public class DefaultRolesMiddleware(RequestDelegate next, IEnumerable<string> roles)
{
    private readonly RequestDelegate _next = next;
    private readonly IEnumerable<string> _roles = roles;

    public async Task InvokeAsync(HttpContext context, RoleManager<IdentityRole> roleManager)
    {
        foreach (var role in _roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));
        }

        await _next(context);
    }
}