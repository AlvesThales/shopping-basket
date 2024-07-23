using MediatR;
using Microsoft.Extensions.Logging;
using ShoppingBasket.Application.Features.Common;
using ShoppingBasket.Application.Interfaces.Repositories;
using ShoppingBasket.Domain.Common;
using ShoppingBasket.Domain.Common.Interfaces;
using ShoppingBasket.Domain.Entities;

namespace ShoppingBasket.Application.Features.ProductFeature.GetProductById;

public class GetProductByIdQuery: IRequest<Result<Product>>
{
    public Guid Id { get; set; }

    public GetProductByIdQuery(Guid id)
    {
        Id = id;
    }
}

public class GetProductQueryHandler : QueryHandler,IRequestHandler<GetProductByIdQuery, Result<Product>>
{
    private readonly ILogger<GetProductQueryHandler> _logger;
    private readonly IMediatorHandler _bus;
    private readonly IProductRepository _productRepository;

    public GetProductQueryHandler(ILogger<GetProductQueryHandler> logger, IUnitOfWork uow, IMediatorHandler bus,
        INotificationHandler<DomainNotification> notifications,IProductRepository productRepository) : base(logger,uow, bus,notifications)
    {
        _logger = logger;
        _bus = bus;
        _productRepository = productRepository;
    }
    
    public async Task<Result<Product>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id, cancellationToken);

        if (product is null)
        {
            return NotifyError("NOT_FOUND", "Product not found", ErrorTypes.NotFound);
        }

        return product;
    }
}