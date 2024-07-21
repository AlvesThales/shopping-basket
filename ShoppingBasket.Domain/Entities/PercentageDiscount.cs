namespace ShoppingBasket.Domain.Entities;

public class PercentageDiscount : Discount
{
    private readonly string _productName;
    private readonly decimal _percentage;

    public PercentageDiscount(string productName, decimal percentage)
    {
        _productName = productName;
        _percentage = percentage;
    }

    public override void ApplyDiscount(Basket basket)
    {
        foreach (var item in basket.BasketItems.Where(i => i.Product.Name == _productName))
        {
            item.Discount = item.UnitPrice * _percentage;
        }
    }
}