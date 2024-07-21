using ShoppingBasket.Application.Extensions;
using ShoppingBasket.Domain;

namespace ShoppingBasket.Application;

public static class ApplicationDependencyInjection
{
    public static IServiceCollection RegisterApplicationDependencyInjection(this IServiceCollection services)
    {
        services
            .RegisterServicesInjection()
            .RegisterCommandsQueriesInjection()
            .RegisterDomainDependencyInjection();
        return services;
    }
}