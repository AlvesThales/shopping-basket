using Riok.Mapperly.Abstractions;
using ShoppingBasket.Application.ViewModels.BasketViewModels;
using ShoppingBasket.Domain.Entities;

namespace ShoppingBasket.Application.Mappings;

[Mapper]
public partial class BasketMapper
{
    public partial CreateBasketOutput BasketToCreateBasketOutput(Basket basket);
    public partial UpdateBasketOutput BasketToUpdateBasketOutput(Basket basket);
    public partial GetBasketSimple BasketToBasketOutput(Basket basket);
}