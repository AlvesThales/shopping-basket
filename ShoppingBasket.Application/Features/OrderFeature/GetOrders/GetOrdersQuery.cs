using MediatR;
using Microsoft.Extensions.Logging;
using ShoppingBasket.Application.Features.OrderFeature.CreateOrder;
using ShoppingBasket.Application.Features.Common;
using ShoppingBasket.Application.Interfaces.Repositories;
using ShoppingBasket.Domain.Common;
using ShoppingBasket.Domain.Common.Interfaces;
using ShoppingBasket.Domain.Entities;

namespace ShoppingBasket.Application.Features.OrderFeature.GetOrder;

public class GetOrdersQuery: IRequest<Result<ICollection<Order>>>
{
    public bool? IsPaid { get; set; }

    public GetOrdersQuery(bool? isPaid)
    {
        IsPaid = isPaid;
    }
}

public class GetOrdersQueryHandler : QueryHandler,IRequestHandler<GetOrdersQuery, Result<ICollection<Order>>>
{
    private readonly ILogger<GetOrderQueryHandler> _logger;
    private readonly IMediatorHandler _bus;
    private readonly IOrderRepository _orderRepository;

    public GetOrdersQueryHandler(ILogger<GetOrderQueryHandler> logger, IUnitOfWork uow, IMediatorHandler bus,
        INotificationHandler<DomainNotification> notifications,IOrderRepository orderRepository) : base(logger,uow, bus,notifications)
    {
        _logger = logger;
        _bus = bus;
        _orderRepository = orderRepository;
    }
    
    public async Task<Result<ICollection<Order>>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        return await _orderRepository.GetAllWithOrderItemsAsync(request.IsPaid, cancellationToken);
    }
}