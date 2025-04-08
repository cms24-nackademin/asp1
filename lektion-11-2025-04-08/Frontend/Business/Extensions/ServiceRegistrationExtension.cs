using Business.Handlers;
using Business.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Business.Extensions;

public static class ServiceRegistrationExtension
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IClientService, ClientService>();
        services.AddScoped<IStatusService, StatusService>();
        services.AddScoped<IProjectService, ProjectService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ITokenHandler, TokenHandler>();
        services.AddScoped(typeof(ICacheHandler<>), typeof(CacheHandler<>));

        return services;
    }
}
