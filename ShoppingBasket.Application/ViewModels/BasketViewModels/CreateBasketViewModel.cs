using ShoppingBasket.Application.ViewModels.BasketItemViewModels;

namespace ShoppingBasket.Application.ViewModels.BasketViewModels;

public record CreateBasketInput(ICollection<CreateBasketItemInput> BasketItems, Guid CustomerId);
public record UpdateBasketInput(Guid BasketId, ICollection<CreateBasketItemInput> BasketItems);
public record CreateBasketOutput(string Id, string CustomerId, ICollection<CreateBasketItemOutput> BasketItems, decimal TotalBasketOriginalPrice, decimal TotalBasketDiscountedPrice, bool IsPaid, bool IsDeleted);
public record UpdateBasketOutput(string Id, string CustomerId, ICollection<CreateBasketItemOutput> BasketItems, decimal TotalBasketOriginalPrice, decimal TotalBasketDiscountedPrice, bool IsPaid, bool IsDeleted);