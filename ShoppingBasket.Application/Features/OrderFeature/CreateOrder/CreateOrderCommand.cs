using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using ShoppingBasket.Application.Features.Common;
using ShoppingBasket.Application.Interfaces.Repositories;
using ShoppingBasket.Application.ViewModels.OrderViewModels;
using ShoppingBasket.Domain.Common;
using ShoppingBasket.Domain.Common.Interfaces;
using ShoppingBasket.Domain.Entities;

namespace ShoppingBasket.Application.Features.OrderFeature.CreateOrder;

public class CreateOrderCommand : Command, IRequest<Result<Order>>
{
    public string CustomerId { get; private set; }
    public virtual ICollection<CreateOrderItemInput> OrderItems { get; private set; }

    public CreateOrderCommand(string customerId, ICollection<CreateOrderItemInput> orderItems)
    {
        CustomerId = customerId;
        OrderItems = orderItems;
    }

    public override bool IsValid()
    {
        ValidationResult = new CreateOrderValidator().Validate(this);
        return ValidationResult.IsValid;
    }
}


public class CreateOrderCommandHandler : CommandHandler, IRequestHandler<CreateOrderCommand, Result<Order>>
{
    private readonly ILogger<CreateOrderCommandHandler> _logger;
    private readonly IMediatorHandler _bus;
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly UserManager<Customer> _userManager;

    public CreateOrderCommandHandler(ILogger<CreateOrderCommandHandler> logger, IUnitOfWork uow, IMediatorHandler bus,
        INotificationHandler<DomainNotification> notifications,IOrderRepository orderRepository, IProductRepository productRepository, UserManager<Customer> userManager) : base(logger,uow, bus,notifications)
    {
        _logger = logger;
        _bus = bus;
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _userManager = userManager;
    }
    
    public async Task<Result<Order>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {

        if (!request.IsValid())
        {
            return NotifyValidationErrors(request);
        }

        var customer = await _userManager.FindByIdAsync(request.CustomerId);
        if (customer == null)
        {
            return NotifyError(GenericErrors.ErrorSaving, "Customer Not Found", ErrorTypes.BadRequest);
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

        _logger.LogDebug("Start creating an order from customer {CustomerName}", request.CustomerId);

        var order = new Order(customer, orderItems, false, false);
        await _orderRepository.AddAsync(order, cancellationToken);

        if (!await Commit(cancellationToken))
        {
            return NotifyError(GenericErrors.ErrorSaving, "Error while saving", ErrorTypes.ServerError);
        }

        return order;
    }
}