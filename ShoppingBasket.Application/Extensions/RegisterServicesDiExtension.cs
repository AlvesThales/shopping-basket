using ShoppingBasket.Application.Interfaces;
using ShoppingBasket.Application.Services;

namespace ShoppingBasket.Application.Extensions;

internal static class RegisterServicesDiExtension
{
    public static IServiceCollection RegisterServicesInjection(this IServiceCollection services)
    {
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IProductService, ProductService>();
        return services;
    }
}