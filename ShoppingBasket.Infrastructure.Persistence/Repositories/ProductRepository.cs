using Microsoft.EntityFrameworkCore;
using ShoppingBasket.Application;
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

    public async Task<Result<ICollection<Product>>> GetProductsAsync(CancellationToken cancellationToken)
    {
        return await _dbSet.ToListAsync(cancellationToken);
    }
}