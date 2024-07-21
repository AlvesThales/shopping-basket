namespace ShoppingBasket.Domain.Entities;

public abstract class Discount
{
    public abstract void ApplyDiscount(Basket basket);
}