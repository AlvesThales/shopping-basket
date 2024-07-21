using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShoppingBasket.Infrastructure.Persistence;
using ShoppingBasket.Domain.Common.Interfaces;

namespace ShoppingBasket.Infrastructure;

public static class InfrastructureDependencyInjection
{
    public static IServiceCollection RegisterInfrastructureDependencyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        
        services
            .AddScoped<IMediatorHandler, MediatorHandler>()
            .RegisterInfrastructurePersistenceDependencyInjection(configuration);

        return services;
    }
}