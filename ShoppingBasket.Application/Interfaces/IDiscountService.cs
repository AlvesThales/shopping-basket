using ShoppingBasket.Domain.Entities;

namespace ShoppingBasket.Application.Interfaces;

public interface IDiscountService
{
    void ApplyDiscounts(Basket basket);
}