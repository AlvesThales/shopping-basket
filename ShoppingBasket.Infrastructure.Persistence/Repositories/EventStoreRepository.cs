using ShoppingBasket.Application.Interfaces.Repositories;
using ShoppingBasket.Domain.Common;
using ShoppingBasket.Infrastructure.Persistence.Context;

namespace ShoppingBasket.Infrastructure.Persistence.Repositories;

public class EventStoreRepository:IEventStoreRepository
{
    private readonly ApplicationDbContext _eventStoreContext;
    
    public EventStoreRepository(ApplicationDbContext eventStoreContext)
    {
        _eventStoreContext = eventStoreContext;
    }
    
    public async Task Store(StoredEvent theEvent, CancellationToken cancellationToken = default)
    {
        await _eventStoreContext.AddAsync(theEvent, cancellationToken);
        await _eventStoreContext.SaveChangesAsync(cancellationToken);
    }
}