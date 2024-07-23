using ShoppingBasket.Core;
using ShoppingBasket.Domain.Entities;

namespace ShoppingBasket.Application.Interfaces.Repositories;

public interface IProductRepository : IGenericRepository<Product>
{
    Task<Result<ICollection<Product>>> GetProductsAsync(CancellationToken cancellationToken);
}