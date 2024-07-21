namespace ShoppingBasket.Domain.Entities;

public class MultiBuyDiscount : Discount
{
    private readonly string _productName;
    private readonly int _requiredAmount;
    private readonly string _discountedProductName;
    private readonly decimal _discountPercentage;

    public MultiBuyDiscount(string productName, int requiredAmount, string discountedProductName, decimal discountPercentage)
    {
        _productName = productName;
        _requiredAmount = requiredAmount;
        _discountedProductName = discountedProductName;
        _discountPercentage = discountPercentage;
    }

    public override void ApplyDiscount(Basket basket)
    {
        var productCount = basket.BasketItems.Where(i => i.Product.Name == _productName).Sum(i => i.Amount);
        var discountedItems = basket.BasketItems.Where(i => i.Product.Name == _discountedProductName).ToList();

        foreach (var item in discountedItems)
        {
            if (productCount >= _requiredAmount)
            {
                item.Discount = item.UnitPrice * _discountPercentage;
                productCount -= _requiredAmount;
            }
            else
            {
                item.Discount = 0;
            }
        }
    }
}