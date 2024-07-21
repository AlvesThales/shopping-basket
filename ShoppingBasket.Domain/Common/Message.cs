using MediatR;

namespace ShoppingBasket.Domain.Common;

public abstract class Message : IRequest
{
    public string MessageType { get; protected set; }
    public Guid AggregateId { get; protected set; }

    protected Message()
    {
        MessageType = GetType().Name;
    }
}