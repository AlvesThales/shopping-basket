using Riok.Mapperly.Abstractions;
using ShoppingBasket.Application.ViewModels.ProductViewModels;
using ShoppingBasket.Domain.Entities;

namespace ShoppingBasket.Application.Mappings;

[Mapper]
public partial class ProductMapper
{
    public partial CreateProductOutput ProductToCreateProductOutput(Product product);
    public partial GetProductSimple ProductToProductOutput(Product product);
}