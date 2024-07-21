using ShoppingBasket.Core;
using ShoppingBasket.Domain.Entities;

namespace ShoppingBasket.Application.Interfaces.Repositories;

public interface IOrderRepository : IGenericRepository<Order>
{
    Task<PaginatedIEnumerable<Order>> GetOrders();
    Task<Order?> GetByIdWithOrderItemsAsync(Guid id, CancellationToken ct = default);
    Task<Result<ICollection<Order>>> GetAllWithOrderItemsAsync(bool? isPaid, CancellationToken cancellationToken);
}