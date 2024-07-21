using ShoppingBasket.Domain.Common;

namespace ShoppingBasket.Application.Interfaces.Repositories;

public interface IEventStoreRepository
{
    Task Store(StoredEvent theEvent, CancellationToken cancellationToken = default);
}