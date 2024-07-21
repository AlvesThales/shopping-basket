using Microsoft.EntityFrameworkCore;
using ShoppingBasket.Core;
using System.Threading;
using ShoppingBasket.Application;
using ShoppingBasket.Application.Interfaces.Repositories;
using ShoppingBasket.Domain.Entities;
using ShoppingBasket.Infrastructure.Persistence.Context;
using ShoppingBasket.Infrastructure.Persistence.GenericRepository;

namespace ShoppingBasket.Infrastructure.Persistence.Repositories;

internal class OrderRepository : Repository<Order>, IOrderRepository
{
    public OrderRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        
    }


    public Task<PaginatedIEnumerable<Order>> GetOrders()
    {
        return _dbSet.AddPagination(0,1);
    }

    public async Task<Order?> GetByIdWithOrderItemsAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dbSet
                    .Where(order => order.Id == id)
                    .Where(order => order.IsDeleted == false)
                    .Include(order => order.Customer)
                    .Include(order => order.OrderItems)
                        .ThenInclude(orderItem => orderItem.Product)
                    .SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<Result<ICollection<Order>>> GetAllWithOrderItemsAsync(bool? isPaid, CancellationToken cancellationToken)
    {
        return await _dbSet
            .Where(order => !isPaid.HasValue || order.IsPaid == isPaid.Value)
            .Where(order => order.IsDeleted == false)
            .Include(order => order.Customer)
            .Include(order => order.OrderItems)
                .ThenInclude(orderItem => orderItem.Product)
            .ToListAsync(cancellationToken);
    }
}