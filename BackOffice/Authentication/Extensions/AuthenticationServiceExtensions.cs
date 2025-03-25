using Authentication.Models;
using Authentication.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Authentication.Extensions;

public static class AuthenticationServiceExtensions
{

    public static IServiceCollection AddLocalAuthentication(this IServiceCollection services, string connectionString, string loginPath = "/auth/login", string accessDeniedPath = "/denied")
    {
        services.AddDbContext<AuthContext>(x => x.UseSqlServer(connectionString));

        services.AddIdentity<AppUser, IdentityRole>(options =>
        {
            options.User.RequireUniqueEmail = true;
            options.Password.RequiredLength = 8;
        })
        .AddDefaultTokenProviders()
        .AddEntityFrameworkStores<AuthContext>();

        services.ConfigureApplicationCookie(options =>
        {
            options.LoginPath = loginPath;
            options.AccessDeniedPath = accessDeniedPath;
            options.SlidingExpiration = true;
            options.ExpireTimeSpan = TimeSpan.FromHours(1);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });

        // Add Services here
        services.AddScoped<IAuthService, AuthService>();

        return services;
    }

}
