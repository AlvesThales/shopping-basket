using ShoppingBasket.Application.ViewModels.BasketItemViewModels;

namespace ShoppingBasket.Application.ViewModels.BasketViewModels;

public record GetBasketSimple(Guid Id, Guid CustomerId, ICollection<GetBasketItemInBasket> BasketItems, decimal TotalBasketOriginalPrice, decimal TotalBasketDiscountedPrice, bool IsPaid, bool IsDeleted);