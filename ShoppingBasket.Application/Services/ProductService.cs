using ShoppingBasket.Application.Features.ProductFeature.CreateProduct;
using ShoppingBasket.Application.Features.ProductFeature.GetProduct;
using ShoppingBasket.Application.Interfaces;
using ShoppingBasket.Application.Mappings;
using ShoppingBasket.Application.ViewModels.ProductViewModels;
using ShoppingBasket.Domain.Common.Interfaces;

namespace ShoppingBasket.Application.Services;

public class ProductService : IProductService
{
    private readonly IMediatorHandler _bus;
    private readonly ProductMapper _mapper;

    public ProductService(IMediatorHandler bus)
    {
        _bus = bus;
        _mapper = new ProductMapper();
    }
    
    public async Task<Result<CreateProductOutput>> CreateProduct(CreateProductInput input, CancellationToken cancellationToken)
    {
        var command = new CreateProductCommand(input.Name, input.Price);
        var response = await _bus.SendCommand(command,cancellationToken);

        return response.Match<Result<CreateProductOutput>>(
            product => _mapper.ProductToCreateProductOutput(product),
            error => error);
    }

    public async Task<Result<GetProductSimple>> GetProduct(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetProductQuery(id);
        var response = await _bus.SendCommand(query, cancellationToken);
        return response.Match<Result<GetProductSimple>>(
            product => _mapper.ProductToProductOutput(product),
            error => error);
    }
}