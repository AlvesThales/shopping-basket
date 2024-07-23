using ShoppingBasket.Application.Interfaces;
using ShoppingBasket.Domain.Entities;

namespace ShoppingBasket.Application.Services;

public class DiscountService : IDiscountService
{
    public void ApplyDiscounts(Basket basket)
    {
        var discounts = new List<Discount>
        {
            new PercentageDiscount("Apples", 0.10m),
            new MultiBuyDiscount("Soup", 2, "Bread", 0.50m)
        };

        basket.ApplyDiscounts(discounts);
    }
}