using System.Text.Json;
using System.Text.Json.Serialization;
using ShoppingBasket.Application.Interfaces.Repositories;
using ShoppingBasket.Domain.Common;

namespace ShoppingBasket.Infrastructure.Persistence.GenericRepository;

public class EventStore:IEventStore
{
    private readonly IEventStoreRepository _eventStoreRepository;

    public EventStore(IEventStoreRepository eventStoreRepository)
    {
        _eventStoreRepository = eventStoreRepository;
    }
    
    public async Task Save<T>(T theEvent) where T : Event
    {
        var serializedData = JsonSerializer.Serialize(theEvent);
        //var user = await _dummyRepository.GetUserLogged();

        var storedEvent = new StoredEvent(
            theEvent,
            serializedData,
            "");
        //user == null ? "" : user.FullName);


        await _eventStoreRepository.Store(storedEvent);
    }
}

public interface IEventStore
{
    Task Save<T>(T theEvent) where T : Event;
}