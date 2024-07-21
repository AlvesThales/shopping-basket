using Microsoft.Extensions.Configuration;
using ShoppingBasket.Infrastructure.Persistence.Extensions;

namespace ShoppingBasket.Infrastructure.Persistence;

public static class InfrastructurePersistenceDependencyInjection
{
    public static IServiceCollection RegisterInfrastructurePersistenceDependencyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistenceLayer(configuration);
        return services;
    }
}