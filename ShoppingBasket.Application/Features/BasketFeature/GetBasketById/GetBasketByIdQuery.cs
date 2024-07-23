using MediatR;
using Microsoft.Extensions.Logging;
using ShoppingBasket.Application.Features.Common;
using ShoppingBasket.Application.Interfaces.Repositories;
using ShoppingBasket.Domain.Common;
using ShoppingBasket.Domain.Common.Interfaces;
using ShoppingBasket.Domain.Entities;

namespace ShoppingBasket.Application.Features.BasketFeature.GetBasketById;

public class GetBasketByIdQuery: IRequest<Result<Basket>>
{
    public Guid Id { get; set; }

    public GetBasketByIdQuery(Guid id)
    {
        Id = id;
    }
}

public class GetBasketByIdQueryHandler : QueryHandler,IRequestHandler<GetBasketByIdQuery, Result<Basket>>
{
    private readonly ILogger<GetBasketByIdQueryHandler> _logger;
    private readonly IMediatorHandler _bus;
    private readonly IBasketRepository _basketRepository;

    public GetBasketByIdQueryHandler(ILogger<GetBasketByIdQueryHandler> logger, IUnitOfWork uow, IMediatorHandler bus,
        INotificationHandler<DomainNotification> notifications,IBasketRepository basketRepository) : base(logger,uow, bus,notifications)
    {
        _logger = logger;
        _bus = bus;
        _basketRepository = basketRepository;
    }
    
    public async Task<Result<Basket>> Handle(GetBasketByIdQuery request, CancellationToken cancellationToken)
    {
        var basket = await _basketRepository.GetByIdWithBasketItemsAsync(request.Id, cancellationToken);

        if (basket is null)
        {
            return NotifyError("NOT_FOUND", "Basket not found", ErrorTypes.NotFound);
        }

        return basket;
    }
}