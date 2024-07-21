using MediatR;
using Microsoft.Extensions.Logging;
using ShoppingBasket.Application.Features.OrderFeature.CreateOrder;
using ShoppingBasket.Application.Features.Common;
using ShoppingBasket.Application.Interfaces.Repositories;
using ShoppingBasket.Domain.Common;
using ShoppingBasket.Domain.Common.Interfaces;
using ShoppingBasket.Domain.Entities;

namespace ShoppingBasket.Application.Features.OrderFeature.GetOrder;

public class GetOrderQuery: IRequest<Result<Order>>
{
    public Guid Id { get; set; }

    public GetOrderQuery(Guid id)
    {
        Id = id;
    }
}

public class GetOrderQueryHandler : QueryHandler,IRequestHandler<GetOrderQuery, Result<Order>>
{
    private readonly ILogger<GetOrderQueryHandler> _logger;
    private readonly IMediatorHandler _bus;
    private readonly IOrderRepository _orderRepository;

    public GetOrderQueryHandler(ILogger<GetOrderQueryHandler> logger, IUnitOfWork uow, IMediatorHandler bus,
        INotificationHandler<DomainNotification> notifications,IOrderRepository orderRepository) : base(logger,uow, bus,notifications)
    {
        _logger = logger;
        _bus = bus;
        _orderRepository = orderRepository;
    }
    
    public async Task<Result<Order>> Handle(GetOrderQuery request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdWithOrderItemsAsync(request.Id, cancellationToken);

        if (order is null)
        {
            return NotifyError("NOT_FOUND", "Order not found", ErrorTypes.NotFound);
        }

        return order;
    }
}