using ShoppingBasket.Application.ViewModels.ProductViewModels;

namespace ShoppingBasket.Application.Interfaces;

public interface IProductService
{
    Task<Result<CreateProductOutput>> CreateProduct(CreateProductInput input, CancellationToken cancellationToken = default);
    Task<Result<GetProductSimple>> GetProduct(Guid id, CancellationToken cancellationToken = default);
}