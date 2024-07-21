using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using ShoppingBasket.Application.Features.Common;
using ShoppingBasket.Application.Interfaces.Repositories;
using ShoppingBasket.Domain.Common;
using ShoppingBasket.Domain.Common.Interfaces;
using ShoppingBasket.Domain.Entities;

namespace ShoppingBasket.Application.Features.BasketFeature.PayBasket;

public class PayBasketCommand : Command, IRequest<Result<Basket>>
{
    public Guid BasketId { get; private set; }

    public PayBasketCommand(Guid basketId)
    {
        BasketId = basketId;
    }

    public override bool IsValid()
    {
        return true;
    }
}


public class PayBasketCommandHandler : CommandHandler, IRequestHandler<PayBasketCommand, Result<Basket>>
{
    private readonly ILogger<PayBasketCommandHandler> _logger;
    private readonly IMediatorHandler _bus;
    private readonly IBasketRepository _basketRepository;
    private readonly IProductRepository _productRepository;
    private readonly UserManager<Customer> _userManager;

    public PayBasketCommandHandler(ILogger<PayBasketCommandHandler> logger, IUnitOfWork uow, IMediatorHandler bus,
        INotificationHandler<DomainNotification> notifications,IBasketRepository basketRepository, IProductRepository productRepository, UserManager<Customer> userManager) : base(logger,uow, bus,notifications)
    {
        _logger = logger;
        _bus = bus;
        _basketRepository = basketRepository;
        _productRepository = productRepository;
        _userManager = userManager;
    }
    
    public async Task<Result<Basket>> Handle(PayBasketCommand request, CancellationToken cancellationToken)
    {

        _logger.LogDebug("Start paying basket with Id {BasketId}", request.BasketId);

        var basket = await _basketRepository.GetByIdWithBasketItemsAsync(request.BasketId, cancellationToken);

        if (basket is null)
        {
            return NotifyError("NOT_FOUND", "Basket not found", ErrorTypes.NotFound);
        }

        if (basket.IsPaid == true)
        {
            return NotifyError("BAD_REQUEST", "Basket already Paid", ErrorTypes.BadRequest);
        }

        Pay(basket);

        if (!await Commit(cancellationToken))
        {
            return NotifyError(GenericErrors.ErrorSaving, "Error while saving", ErrorTypes.ServerError);
        }

        return basket;
    }

    private static void Pay(Basket? basket)
    {
        basket.PaidAmount = basket.TotalPrice;
        basket.IsPaid = true;
    }
}