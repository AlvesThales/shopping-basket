using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using ShoppingBasket.Application.Features.Common;
using ShoppingBasket.Application.Interfaces.Repositories;
using ShoppingBasket.Application.ViewModels.OrderViewModels;
using ShoppingBasket.Domain.Common;
using ShoppingBasket.Domain.Common.Interfaces;
using ShoppingBasket.Domain.Entities;

namespace ShoppingBasket.Application.Features.OrderFeature.UpdateOrder;

public class UpdateOrderCommand : Command, IRequest<Result<Order>>
{
    public Guid OrderId { get; private set; }
    public virtual ICollection<CreateOrderItemInput> OrderItems { get; private set; }

    public UpdateOrderCommand(Guid customerId, ICollection<CreateOrderItemInput> orderItems)
    {
        OrderId = customerId;
        OrderItems = orderItems;
    }

    public override bool IsValid()
    {
        ValidationResult = new UpdateOrderValidator().Validate(this);
        return ValidationResult.IsValid;
    }
}


public class UpdateOrderCommandHandler : CommandHandler, IRequestHandler<UpdateOrderCommand, Result<Order>>
{
    private readonly ILogger<UpdateOrderCommandHandler> _logger;
    private readonly IMediatorHandler _bus;
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly UserManager<Customer> _userManager;

    public UpdateOrderCommandHandler(ILogger<UpdateOrderCommandHandler> logger, IUnitOfWork uow, IMediatorHandler bus,
        INotificationHandler<DomainNotification> notifications,IOrderRepository orderRepository, IProductRepository productRepository, UserManager<Customer> userManager) : base(logger,uow, bus,notifications)
    {
        _logger = logger;
        _bus = bus;
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _userManager = userManager;
    }
    
    public async Task<Result<Order>> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {

        if (!request.IsValid())
        {
            return NotifyValidationErrors(request);
        }

        var order = await _orderRepository.GetByIdWithOrderItemsAsync(request.OrderId, cancellationToken);

        if (order is null)
        {
            return NotifyError("NOT_FOUND", "Order not found", ErrorTypes.NotFound);
        }

        var orderItems = new List<OrderItem>();

        foreach (var item in request.OrderItems) 
        {
            var product = await _productRepository.GetByIdAsync(item.ProductId);
            if (product == null)
            {
                return NotifyError(GenericErrors.ErrorSaving, $"Product with Id {item.ProductId} Not Found", ErrorTypes.BadRequest);
            }
            orderItems.Add(new OrderItem(product, product.Price, item.Amount));
        }

        try
        {
            order.UpdateOrderItems(orderItems);
        }
        catch (Exception ex)
        {
            return NotifyError(GenericErrors.ErrorSaving, ex.Message, ErrorTypes.ServerError);
        }

        _logger.LogDebug("Start updating order with Id {OrderId}", request.OrderId);

        await _orderRepository.Update(order);
      
        if (!await Commit(cancellationToken))
        {
            return NotifyError(GenericErrors.ErrorSaving, "Error while saving", ErrorTypes.ServerError);
        }

        return order;
    }
}