using MediatR;
using ShoppingBasket.Domain.Common;
using ShoppingBasket.Domain.Common.Interfaces;
using ShoppingBasket.Infrastructure.Persistence.GenericRepository;

namespace ShoppingBasket.Infrastructure;

public class MediatorHandler : IMediatorHandler
{
    private readonly IMediator _mediator;
    private readonly IEventStore _eventStore;

    public MediatorHandler(IMediator mediator, IEventStore eventStore)
    {
        _mediator = mediator;
        _eventStore = eventStore;
    }
    public Task<T> SendCommand<T>(IRequest<T> command, CancellationToken cancellationToken = default)
    {
        return _mediator.Send(command, cancellationToken);
    }
    public async Task RaiseEvent<T>(T @event) where T : Event
    {
        await _mediator.Publish(@event);

        if (!@event.MessageType.Equals("DomainNotification"))
        {
            await _eventStore?.Save(@event)!;
        }

    }
}