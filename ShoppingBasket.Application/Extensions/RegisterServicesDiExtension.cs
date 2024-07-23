using ShoppingBasket.Application.Interfaces;
using ShoppingBasket.Application.Services;

namespace ShoppingBasket.Application.Extensions;

internal static class RegisterServicesDiExtension
{
    public static IServiceCollection RegisterServicesInjection(this IServiceCollection services)
    {
        services.AddScoped<IBasketService, BasketService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IDiscountService, DiscountService>();
        return services;
    }
}