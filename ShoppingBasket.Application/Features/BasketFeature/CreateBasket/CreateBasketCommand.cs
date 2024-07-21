using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using ShoppingBasket.Application.Features.Common;
using ShoppingBasket.Application.Interfaces.Repositories;
using ShoppingBasket.Application.ViewModels.BasketItemViewModels;
using ShoppingBasket.Domain.Common;
using ShoppingBasket.Domain.Common.Interfaces;
using ShoppingBasket.Domain.Entities;

namespace ShoppingBasket.Application.Features.BasketFeature.CreateBasket;

public class CreateBasketCommand : Command, IRequest<Result<Basket>>
{
    public string CustomerId { get; private set; }
    public virtual ICollection<CreateBasketItemInput> BasketItems { get; private set; }

    public CreateBasketCommand(string customerId, ICollection<CreateBasketItemInput> basketItems)
    {
        CustomerId = customerId;
        BasketItems = basketItems;
    }

    public override bool IsValid()
    {
        ValidationResult = new CreateBasketValidator().Validate(this);
        return ValidationResult.IsValid;
    }
}


public class CreateBasketCommandHandler : CommandHandler, IRequestHandler<CreateBasketCommand, Result<Basket>>
{
    private readonly ILogger<CreateBasketCommandHandler> _logger;
    private readonly IMediatorHandler _bus;
    private readonly IBasketRepository _basketRepository;
    private readonly IProductRepository _productRepository;
    private readonly UserManager<Customer> _userManager;

    public CreateBasketCommandHandler(ILogger<CreateBasketCommandHandler> logger, IUnitOfWork uow, IMediatorHandler bus,
        INotificationHandler<DomainNotification> notifications,IBasketRepository basketRepository, IProductRepository productRepository, UserManager<Customer> userManager) : base(logger,uow, bus,notifications)
    {
        _logger = logger;
        _bus = bus;
        _basketRepository = basketRepository;
        _productRepository = productRepository;
        _userManager = userManager;
    }
    
    public async Task<Result<Basket>> Handle(CreateBasketCommand request, CancellationToken cancellationToken)
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

        var basketItems = new List<BasketItem>();

        foreach (var item in request.BasketItems) 
        {
            var product = await _productRepository.GetByIdAsync(item.ProductId);
            if (product == null)
            {
                return NotifyError(GenericErrors.ErrorSaving, $"Product with Id {item.ProductId} Not Found", ErrorTypes.BadRequest);
            }
            basketItems.Add(new BasketItem(product, product.Price, item.Amount));
        }

        _logger.LogDebug("Start creating a basket from customer {CustomerName}", request.CustomerId);

        var basket = new Basket(customer, basketItems, false, false);
        await _basketRepository.AddAsync(basket, cancellationToken);

        if (!await Commit(cancellationToken))
        {
            return NotifyError(GenericErrors.ErrorSaving, "Error while saving", ErrorTypes.ServerError);
        }

        return basket;
    }
}