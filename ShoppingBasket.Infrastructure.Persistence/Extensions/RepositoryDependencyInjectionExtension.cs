using ShoppingBasket.Application.Interfaces.Repositories;
using ShoppingBasket.Infrastructure.Persistence.Repositories;

namespace ShoppingBasket.Infrastructure.Persistence.Extensions;

internal static class RepositoryDependencyInjectionExtension
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        return services
            .AddScoped<IOrderRepository, OrderRepository>()
            .AddScoped<IProductRepository, ProductRepository>()
            .AddScoped<IEventStoreRepository, EventStoreRepository>();
    }
}