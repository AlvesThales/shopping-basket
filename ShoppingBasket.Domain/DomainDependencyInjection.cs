using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ShoppingBasket.Domain.Common;

namespace ShoppingBasket.Domain;

public static class DomainDependencyInjection
{
    public static IServiceCollection RegisterDomainDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
        return services;
    }
}