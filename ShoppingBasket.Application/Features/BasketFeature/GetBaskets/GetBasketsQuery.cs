using MediatR;
using Microsoft.Extensions.Logging;
using ShoppingBasket.Application.Features.Common;
using ShoppingBasket.Application.Interfaces.Repositories;
using ShoppingBasket.Domain.Common;
using ShoppingBasket.Domain.Common.Interfaces;
using ShoppingBasket.Domain.Entities;

namespace ShoppingBasket.Application.Features.BasketFeature.GetBaskets;

public class GetBasketsQuery: IRequest<Result<ICollection<Basket>>>
{
    public bool? IsPaid { get; set; }

    public GetBasketsQuery(bool? isPaid)
    {
        IsPaid = isPaid;
    }
}

public class GetBasketsQueryHandler : QueryHandler,IRequestHandler<GetBasketsQuery, Result<ICollection<Basket>>>
{
    private readonly ILogger<GetBasketsQueryHandler> _logger;
    private readonly IMediatorHandler _bus;
    private readonly IBasketRepository _basketRepository;

    public GetBasketsQueryHandler(ILogger<GetBasketsQueryHandler> logger, IUnitOfWork uow, IMediatorHandler bus,
        INotificationHandler<DomainNotification> notifications,IBasketRepository basketRepository) : base(logger,uow, bus,notifications)
    {
        _logger = logger;
        _bus = bus;
        _basketRepository = basketRepository;
    }
    
    public async Task<Result<ICollection<Basket>>> Handle(GetBasketsQuery request, CancellationToken cancellationToken)
    {
        return await _basketRepository.GetAllWithBasketItemsAsync(request.IsPaid, cancellationToken);
    }
}