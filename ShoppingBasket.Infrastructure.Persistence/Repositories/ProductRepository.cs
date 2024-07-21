using ShoppingBasket.Core;
using ShoppingBasket.Application.Interfaces.Repositories;
using ShoppingBasket.Domain.Entities;
using ShoppingBasket.Infrastructure.Persistence.Context;
using ShoppingBasket.Infrastructure.Persistence.GenericRepository;

namespace ShoppingBasket.Infrastructure.Persistence.Repositories;

internal class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        
    }


    public Task<PaginatedIEnumerable<Product>> GetProducts()
    {
        return _dbSet.AddPagination(0,1);
    }
}