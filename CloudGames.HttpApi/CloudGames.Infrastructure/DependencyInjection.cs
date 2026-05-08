using CloudGames.Domain.Repositories;
using CloudGames.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CloudGames.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInjectionServices(this IServiceCollection services)
    {
        services.AddScoped<IGameRepository, GameRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}
