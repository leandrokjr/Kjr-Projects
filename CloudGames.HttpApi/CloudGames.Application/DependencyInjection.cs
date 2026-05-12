using CloudGames.Application.Interfaces;
using CloudGames.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CloudGames.Application;

public static class DependencyInjectionApplication
{
    public static IServiceCollection AddInjectionServicesApplication(this IServiceCollection services)
    {
        services.AddScoped<IGameService, GameService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthService, AuthService>();

        return services;
    }
}
