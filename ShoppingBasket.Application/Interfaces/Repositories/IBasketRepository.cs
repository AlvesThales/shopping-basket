using ShoppingBasket.Core;
using ShoppingBasket.Domain.Entities;

namespace ShoppingBasket.Application.Interfaces.Repositories;

public interface IBasketRepository : IGenericRepository<Basket>
{
    Task<PaginatedIEnumerable<Basket>> GetBaskets();
    Task<Basket?> GetByIdWithBasketItemsAsync(Guid id, CancellationToken ct = default);
    Task<Result<ICollection<Basket>>> GetAllWithBasketItemsAsync(string userId, bool? isPaid,
        CancellationToken cancellationToken);
}