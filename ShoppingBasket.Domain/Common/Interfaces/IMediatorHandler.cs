using MediatR;

namespace ShoppingBasket.Domain.Common.Interfaces;

public interface IMediatorHandler
{

    Task<T> SendCommand<T>(IRequest<T> command, CancellationToken cancellationToken = default);
    Task RaiseEvent<T>(T @event) where T : Event;

}