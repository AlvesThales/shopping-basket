using MediatR;
using Microsoft.Extensions.Logging;
using ShoppingBasket.Application.Features.Common;
using ShoppingBasket.Application.Interfaces.Repositories;
using ShoppingBasket.Domain.Common;
using ShoppingBasket.Domain.Common.Interfaces;
using ShoppingBasket.Domain.Entities;

namespace ShoppingBasket.Application.Features.ProductFeature.CreateProduct;

public class CreateProductCommand : Command, IRequest<Result<Product>>
{
    public string Name { get; private set; }
    public Decimal Price { get; private set; }

    public CreateProductCommand(string name, decimal price)
    {
        Name = name;
        Price = price;
    }

    public override bool IsValid()
    {
        ValidationResult = new CreateProductValidator().Validate(this);
        return ValidationResult.IsValid;
    }
}


public class CreateProductCommandHandler : CommandHandler, IRequestHandler<CreateProductCommand, Result<Product>>
{
    private readonly ILogger<CreateProductCommandHandler> _logger;
    private readonly IMediatorHandler _bus;
    private readonly IProductRepository _productRepository;

    public CreateProductCommandHandler(ILogger<CreateProductCommandHandler> logger, IUnitOfWork uow, IMediatorHandler bus,
        INotificationHandler<DomainNotification> notifications,IProductRepository productRepository) : base(logger,uow, bus,notifications)
    {
        _logger = logger;
        _bus = bus;
        _productRepository = productRepository;
    }
    
    public async Task<Result<Product>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {

        if (!request.IsValid())
        {
            return NotifyValidationErrors(request);
        }
        
        _logger.LogDebug("Start creating a product of name {ProductName}", request.Name);

        var product = new Product(request.Name, request.Price);
        await _productRepository.AddAsync(product, cancellationToken);

        if (!await Commit(cancellationToken))
        {
            return NotifyError(GenericErrors.ErrorSaving, "Error while saving", ErrorTypes.ServerError);
        }
        
        return product;
    }
}