using Data.Contexts;
using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Data.Extensions;

public static class AuthenticationRegistrationExtension
{
    public static IServiceCollection AddLocalIdentity(this IServiceCollection services)
    {
        services.AddIdentity<UserEntity, IdentityRole>(x =>
        {
            x.User.RequireUniqueEmail = true;
            x.Password.RequiredLength = 8;
        })
        .AddEntityFrameworkStores<DataContext>()
        .AddDefaultTokenProviders();

        services.ConfigureApplicationCookie(x =>
        {
            x.LoginPath = "/auth/login";
            x.AccessDeniedPath = "/auth/denied";
            x.ExpireTimeSpan = TimeSpan.FromMinutes(30);
            x.SlidingExpiration = true;
            x.Cookie.HttpOnly = true;
            x.Cookie.IsEssential = true;
        });

        return services;
    }
}
