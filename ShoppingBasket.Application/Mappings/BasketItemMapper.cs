using Riok.Mapperly.Abstractions;
using ShoppingBasket.Application.ViewModels.BasketItemViewModels;
using ShoppingBasket.Domain.Entities;

namespace ShoppingBasket.Application.Mappings;

[Mapper]
public partial class BasketItemMapper
{
    public partial CreateBasketItemOutput BasketItemToCreateBasketItemOutput(Basket basketItem);
    public partial GetBasketItemInBasket BasketItemToBasketItemOutput(BasketItem basketItem);

}