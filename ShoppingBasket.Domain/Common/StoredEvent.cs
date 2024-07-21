namespace ShoppingBasket.Domain.Common;

public class StoredEvent : Event
{
    public Guid Id { get; private set; }
    public string? Data { get; private set; }
    public string? User { get; private set; }
    public string? FullName { get; private set; }
    public bool IsAdmin { get; private set; }

    public StoredEvent(Event theEvent, string data, string user)
    {
        Id = Guid.NewGuid();
        AggregateId = theEvent.AggregateId;
        MessageType = theEvent.MessageType;
        Data = data;
        User = user;
    }

    // EF Constructor
    protected StoredEvent() { }
}