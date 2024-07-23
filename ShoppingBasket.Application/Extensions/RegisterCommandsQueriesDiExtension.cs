using MediatR;
using ShoppingBasket.Application.Features.BasketFeature.CreateBasket;
using ShoppingBasket.Application.Features.BasketFeature.DeleteBasket;
using ShoppingBasket.Application.Features.BasketFeature.GetBasketById;
using ShoppingBasket.Application.Features.BasketFeature.GetBaskets;
using ShoppingBasket.Application.Features.BasketFeature.PayBasket;
using ShoppingBasket.Application.Features.BasketFeature.UpdateBasket;
using ShoppingBasket.Application.Features.ProductFeature.CreateProduct;
using ShoppingBasket.Application.Features.ProductFeature.GetProductById;
using ShoppingBasket.Application.Features.ProductFeature.GetProducts;
using ShoppingBasket.Domain.Entities;

namespace ShoppingBasket.Application.Extensions;

internal static class RegisterCommandsQueriesDiExtension
{
    public static IServiceCollection RegisterCommandsQueriesInjection(this IServiceCollection services)
    {
        services.AddScoped<IRequestHandler<CreateBasketCommand, Result<Basket>>, CreateBasketCommandHandler>();
        services.AddScoped<IRequestHandler<PayBasketCommand, Result<Basket>>, PayBasketCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateBasketCommand, Result<Basket>>, UpdateBasketCommandHandler>();
        services.AddScoped<IRequestHandler<DeleteBasketCommand, Result<Unit>>, DeleteBasketCommandHandler>();
        services.AddScoped<IRequestHandler<CreateProductCommand, Result<Product>>, CreateProductCommandHandler>();
        services.AddScoped<IRequestHandler<GetBasketByIdQuery, Result<Basket>>, GetBasketByIdQueryHandler>();
        services.AddScoped<IRequestHandler<GetBasketsQuery, Result<ICollection<Basket>>>, GetBasketsQueryHandler>();
        services.AddScoped<IRequestHandler<GetProductByIdQuery, Result<Product>>, GetProductQueryHandler>();
        services.AddScoped<IRequestHandler<GetProductsQuery, Result<ICollection<Product>>>, GetProductsQueryHandler>();
        return services;
    }
}