using ShoppingBasket.Application.ViewModels.ProductViewModels;

namespace ShoppingBasket.Application.Interfaces;

public interface IProductService
{
    Task<Result<CreateProductOutput>> CreateProduct(CreateProductInput input, CancellationToken cancellationToken = default);
    Task<Result<GetProductSimple>> GetProductById(Guid id, CancellationToken cancellationToken = default);
    Task<Result<ICollection<GetProductSimple>>> GetProducts(CancellationToken cancellationToken);

}