using MediatR;
using Microsoft.Extensions.Logging;
using ShoppingBasket.Application.Features.Common;
using ShoppingBasket.Application.Interfaces.Repositories;
using ShoppingBasket.Domain.Common;
using ShoppingBasket.Domain.Common.Interfaces;
using ShoppingBasket.Domain.Entities;

namespace ShoppingBasket.Application.Features.ProductFeature.GetProducts;

public class GetProductsQuery: IRequest<Result<ICollection<Product>>>
{
    public GetProductsQuery()
    {
    }
}

public class GetProductsQueryHandler : QueryHandler,IRequestHandler<GetProductsQuery, Result<ICollection<Product>>>
{
    private readonly ILogger<GetProductsQueryHandler> _logger;
    private readonly IMediatorHandler _bus;
    private readonly IProductRepository _productRepository;

    public GetProductsQueryHandler(ILogger<GetProductsQueryHandler> logger, IUnitOfWork uow, IMediatorHandler bus,
        INotificationHandler<DomainNotification> notifications,IProductRepository productRepository) : base(logger,uow, bus,notifications)
    {
        _logger = logger;
        _bus = bus;
        _productRepository = productRepository;
    }
    
    public async Task<Result<ICollection<Product>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        return await _productRepository.GetProductsAsync(cancellationToken);
    }
}