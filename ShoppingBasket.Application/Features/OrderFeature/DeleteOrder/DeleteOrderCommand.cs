using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using ShoppingBasket.Application.Features.Common;
using ShoppingBasket.Application.Interfaces.Repositories;
using ShoppingBasket.Domain.Common;
using ShoppingBasket.Domain.Common.Interfaces;
using ShoppingBasket.Domain.Entities;

namespace ShoppingBasket.Application.Features.OrderFeature.DeleteOrder;

public class DeleteOrderCommand : Command, IRequest<Result<Unit>>
{
    public Guid OrderId { get; private set; }

    public DeleteOrderCommand(Guid orderId)
    {
        OrderId = orderId;
    }

    public override bool IsValid()
    {
        return true;
    }
}


public class DeleteOrderCommandHandler : CommandHandler, IRequestHandler<DeleteOrderCommand, Result<Unit>>
{
    private readonly ILogger<DeleteOrderCommandHandler> _logger;
    private readonly IMediatorHandler _bus;
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly UserManager<Customer> _userManager;

    public DeleteOrderCommandHandler(ILogger<DeleteOrderCommandHandler> logger, IUnitOfWork uow, IMediatorHandler bus,
        INotificationHandler<DomainNotification> notifications,IOrderRepository orderRepository, IProductRepository productRepository, UserManager<Customer> userManager) : base(logger,uow, bus,notifications)
    {
        _logger = logger;
        _bus = bus;
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _userManager = userManager;
    }
    
    public async Task<Result<Unit>> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
       
        _logger.LogDebug("Start deleting order with Id {OrderId}", request.OrderId);

        var order = await _orderRepository.GetByIdWithOrderItemsAsync(request.OrderId, cancellationToken);

        if (order is null)
        {
            return NotifyError("NOT_FOUND", "Order not found", ErrorTypes.NotFound);
        }

        order.IsDeleted = true;

        if (!await Commit(cancellationToken))
        {
            return NotifyError(GenericErrors.ErrorSaving, "Error while saving", ErrorTypes.ServerError);
        }

        return await Unit.Task;
    }
}