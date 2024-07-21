using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using ShoppingBasket.Application.Features.Common;
using ShoppingBasket.Application.Interfaces.Repositories;
using ShoppingBasket.Domain.Common;
using ShoppingBasket.Domain.Common.Interfaces;
using ShoppingBasket.Domain.Entities;

namespace ShoppingBasket.Application.Features.BasketFeature.DeleteBasket;

public class DeleteBasketCommand : Command, IRequest<Result<Unit>>
{
    public Guid BasketId { get; private set; }

    public DeleteBasketCommand(Guid basketId)
    {
        BasketId = basketId;
    }

    public override bool IsValid()
    {
        return true;
    }
}


public class DeleteBasketCommandHandler : CommandHandler, IRequestHandler<DeleteBasketCommand, Result<Unit>>
{
    private readonly ILogger<DeleteBasketCommandHandler> _logger;
    private readonly IMediatorHandler _bus;
    private readonly IBasketRepository _basketRepository;
    private readonly IProductRepository _productRepository;
    private readonly UserManager<Customer> _userManager;

    public DeleteBasketCommandHandler(ILogger<DeleteBasketCommandHandler> logger, IUnitOfWork uow, IMediatorHandler bus,
        INotificationHandler<DomainNotification> notifications,IBasketRepository basketRepository, IProductRepository productRepository, UserManager<Customer> userManager) : base(logger,uow, bus,notifications)
    {
        _logger = logger;
        _bus = bus;
        _basketRepository = basketRepository;
        _productRepository = productRepository;
        _userManager = userManager;
    }
    
    public async Task<Result<Unit>> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
    {
       
        _logger.LogDebug("Start deleting basket with Id {BasketId}", request.BasketId);

        var basket = await _basketRepository.GetByIdWithBasketItemsAsync(request.BasketId, cancellationToken);

        if (basket is null)
        {
            return NotifyError("NOT_FOUND", "Basket not found", ErrorTypes.NotFound);
        }

        basket.IsDeleted = true;

        if (!await Commit(cancellationToken))
        {
            return NotifyError(GenericErrors.ErrorSaving, "Error while saving", ErrorTypes.ServerError);
        }

        return await Unit.Task;
    }
}