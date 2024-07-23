using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using ShoppingBasket.Application.Features.Common;
using ShoppingBasket.Application.Interfaces;
using ShoppingBasket.Application.Interfaces.Repositories;
using ShoppingBasket.Application.ViewModels.BasketItemViewModels;
using ShoppingBasket.Domain.Common;
using ShoppingBasket.Domain.Common.Interfaces;
using ShoppingBasket.Domain.Entities;

namespace ShoppingBasket.Application.Features.BasketFeature.UpdateBasket;

public class UpdateBasketCommand : Command, IRequest<Result<Basket>>
{
    public Guid BasketId { get; private set; }
    public virtual ICollection<CreateBasketItemInput> BasketItems { get; private set; }

    public UpdateBasketCommand(Guid customerId, ICollection<CreateBasketItemInput> basketItems)
    {
        BasketId = customerId;
        BasketItems = basketItems;
    }

    public override bool IsValid()
    {
        ValidationResult = new UpdateBasketValidator().Validate(this);
        return ValidationResult.IsValid;
    }
}


public class UpdateBasketCommandHandler : CommandHandler, IRequestHandler<UpdateBasketCommand, Result<Basket>>
{
    private readonly ILogger<UpdateBasketCommandHandler> _logger;
    private readonly IMediatorHandler _bus;
    private readonly IBasketRepository _basketRepository;
    private readonly IProductRepository _productRepository;
    private readonly UserManager<Customer> _userManager;
    private readonly IDiscountService _discountService;


    public UpdateBasketCommandHandler(ILogger<UpdateBasketCommandHandler> logger, IUnitOfWork uow, IMediatorHandler bus,
        INotificationHandler<DomainNotification> notifications,IBasketRepository basketRepository, IProductRepository productRepository,
        UserManager<Customer> userManager, IDiscountService discountService) : base(logger,uow, bus,notifications)
    {
        _logger = logger;
        _bus = bus;
        _basketRepository = basketRepository;
        _productRepository = productRepository;
        _userManager = userManager;
        _discountService = discountService;
    }
    
    public async Task<Result<Basket>> Handle(UpdateBasketCommand request, CancellationToken cancellationToken)
    {

        if (!request.IsValid())
        {
            return NotifyValidationErrors(request);
        }

        var basket = await _basketRepository.GetByIdWithBasketItemsAsync(request.BasketId, cancellationToken);

        if (basket is null)
        {
            return NotifyError("NOT_FOUND", "Basket not found", ErrorTypes.NotFound);
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

        try
        {
            basket.UpdateBasketItems(basketItems);
            
            _discountService.ApplyDiscounts(basket);
        }
        catch (Exception ex)
        {
            return NotifyError(GenericErrors.ErrorSaving, ex.Message, ErrorTypes.ServerError);
        }

        _logger.LogDebug("Start updating basket with Id {BasketId}", request.BasketId);

        await _basketRepository.Update(basket);
      
        if (!await Commit(cancellationToken))
        {
            return NotifyError(GenericErrors.ErrorSaving, "Error while saving", ErrorTypes.ServerError);
        }

        return basket;
    }
}