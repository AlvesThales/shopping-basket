using MediatR;
using ShoppingBasket.Application.Features.OrderFeature.CreateOrder;
using ShoppingBasket.Application.Features.OrderFeature.DeleteOrder;
using ShoppingBasket.Application.Features.OrderFeature.GetOrder;
using ShoppingBasket.Application.Features.OrderFeature.PayOrder;
using ShoppingBasket.Application.Features.OrderFeature.UpdateOrder;
using ShoppingBasket.Application.Features.ProductFeature.CreateProduct;
using ShoppingBasket.Application.Features.ProductFeature.GetProduct;
using ShoppingBasket.Domain.Entities;

namespace ShoppingBasket.Application.Extensions;

internal static class RegisterCommandsQueriesDiExtension
{
    public static IServiceCollection RegisterCommandsQueriesInjection(this IServiceCollection services)
    {
        services.AddScoped<IRequestHandler<CreateOrderCommand, Result<Order>>, CreateOrderCommandHandler>();
        services.AddScoped<IRequestHandler<PayOrderCommand, Result<Order>>, PayOrderCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateOrderCommand, Result<Order>>, UpdateOrderCommandHandler>();
        services.AddScoped<IRequestHandler<DeleteOrderCommand, Result<Unit>>, DeleteOrderCommandHandler>();
        services.AddScoped<IRequestHandler<CreateProductCommand, Result<Product>>, CreateProductCommandHandler>();
        services.AddScoped<IRequestHandler<GetOrderQuery, Result<Order>>, GetOrderQueryHandler>();
        services.AddScoped<IRequestHandler<GetOrdersQuery, Result<ICollection<Order>>>, GetOrdersQueryHandler>();
        services.AddScoped<IRequestHandler<GetProductQuery, Result<Product>>, GetProductQueryHandler>();
        return services;
    }
}