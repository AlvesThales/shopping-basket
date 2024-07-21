using ShoppingBasket.Core;
using ShoppingBasket.Domain.Entities;

namespace ShoppingBasket.Application.Interfaces.Repositories;

public interface IProductRepository : IGenericRepository<Product>
{
    Task<PaginatedIEnumerable<Product>> GetProducts();
}