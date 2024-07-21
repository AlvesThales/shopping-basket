using MediatR;
using ShoppingBasket.Application.ViewModels.OrderViewModels;

namespace ShoppingBasket.Application.Interfaces;

public interface IOrderService
{
    Task<Result<CreateOrderOutput>> CreateOrder(string customerId, CreateOrderInput input, CancellationToken cancellationToken = default);
    Task<Result<UpdateOrderOutput>> UpdateOrder(Guid orderId, UpdateOrderInput input, CancellationToken cancellationToken = default);
    Task<Result<GetOrderSimple>> GetOrder(Guid id, CancellationToken cancellationToken = default);
    Task<Result<CreateOrderOutput>> PayOrder(Guid id, CancellationToken cancellationToken = default);
    Task<Result<ICollection<GetOrderSimple>>> GetOrders(bool? isPaid, CancellationToken cancellationToken = default);
    Task<Result<Unit>> DeleteOrder(Guid id, CancellationToken cancellationToken);
}