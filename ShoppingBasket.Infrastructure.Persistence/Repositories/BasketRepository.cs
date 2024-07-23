using Microsoft.EntityFrameworkCore;
using ShoppingBasket.Core;
using ShoppingBasket.Application;
using ShoppingBasket.Application.Interfaces.Repositories;
using ShoppingBasket.Domain.Entities;
using ShoppingBasket.Infrastructure.Persistence.Context;
using ShoppingBasket.Infrastructure.Persistence.GenericRepository;

namespace ShoppingBasket.Infrastructure.Persistence.Repositories;

internal class BasketRepository : Repository<Basket>, IBasketRepository
{
    public BasketRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        
    }


    public Task<PaginatedIEnumerable<Basket>> GetBaskets()
    {
        return _dbSet.AddPagination(0,1);
    }

    public async Task<Basket?> GetByIdWithBasketItemsAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dbSet
                    .Where(basket => basket.Id == id)
                    .Where(basket => basket.IsDeleted == false)
                    .Include(basket => basket.Customer)
                    .Include(basket => basket.BasketItems)
                        .ThenInclude(basketItem => basketItem.Product)
                    .SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<Result<ICollection<Basket>>> GetAllWithBasketItemsAsync(string userId, bool? isPaid,
        CancellationToken cancellationToken)
    {
        return await _dbSet
            .Include(basket => basket.Customer)
            .Include(basket => basket.BasketItems)
                .ThenInclude(basketItem => basketItem.Product)
            .Where(basket => !isPaid.HasValue || basket.IsPaid == isPaid.Value)
            .Where(basket => basket.IsDeleted == false)
            .Where(basket => basket.Customer.Id.Equals(userId))
            .ToListAsync(cancellationToken);
    }
}