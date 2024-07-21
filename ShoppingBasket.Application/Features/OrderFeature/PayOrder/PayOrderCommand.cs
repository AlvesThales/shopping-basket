using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using ShoppingBasket.Application.ViewModels.OrderViewModels;
using ShoppingBasket.Application.Features.Common;
using ShoppingBasket.Application.Interfaces.Repositories;
using ShoppingBasket.Domain.Common;
using ShoppingBasket.Domain.Common.Interfaces;
using ShoppingBasket.Domain.Entities;

namespace ShoppingBasket.Application.Features.OrderFeature.PayOrder;

public class PayOrderCommand : Command, IRequest<Result<Order>>
{
    public Guid OrderId { get; private set; }

    public PayOrderCommand(Guid orderId)
    {
        OrderId = orderId;
    }

    public override bool IsValid()
    {
        return true;
    }
}


public class PayOrderCommandHandler : CommandHandler, IRequestHandler<PayOrderCommand, Result<Order>>
{
    private readonly ILogger<PayOrderCommandHandler> _logger;
    private readonly IMediatorHandler _bus;
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly UserManager<Customer> _userManager;

    public PayOrderCommandHandler(ILogger<PayOrderCommandHandler> logger, IUnitOfWork uow, IMediatorHandler bus,
        INotificationHandler<DomainNotification> notifications,IOrderRepository orderRepository, IProductRepository productRepository, UserManager<Customer> userManager) : base(logger,uow, bus,notifications)
    {
        _logger = logger;
        _bus = bus;
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _userManager = userManager;
    }
    
    public async Task<Result<Order>> Handle(PayOrderCommand request, CancellationToken cancellationToken)
    {

        _logger.LogDebug("Start paying order with Id {OrderId}", request.OrderId);

        var order = await _orderRepository.GetByIdWithOrderItemsAsync(request.OrderId, cancellationToken);

        if (order is null)
        {
            return NotifyError("NOT_FOUND", "Order not found", ErrorTypes.NotFound);
        }

        if (order.IsPaid == true)
        {
            return NotifyError("BAD_REQUEST", "Order already Paid", ErrorTypes.BadRequest);
        }

        Pay(order);

        if (!await Commit(cancellationToken))
        {
            return NotifyError(GenericErrors.ErrorSaving, "Error while saving", ErrorTypes.ServerError);
        }

        return order;
    }

    private static void Pay(Order? order)
    {
        order.PaidAmount = order.TotalPrice;
        order.IsPaid = true;
    }
}