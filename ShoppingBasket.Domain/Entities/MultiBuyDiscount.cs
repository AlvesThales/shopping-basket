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
        // Calculate the total quantity of the required product
        var productCount = basket.BasketItems
            .Where(i => i.Product.Name == _productName)
            .Sum(i => i.Amount);

        // Get the single item eligible for the discount
        var discountedItem = basket.BasketItems
            .FirstOrDefault(i => i.Product.Name == _discountedProductName);

        if (discountedItem != null)
        {
            // Calculate the number of discounted items based on the available quantity of the required product
            var discountableQuantity = productCount / _requiredAmount;

            // Apply the discount to the eligible quantity
            discountedItem.TotalDiscount = discountedItem.UnitPrice * _discountPercentage * Math.Min(discountableQuantity, discountedItem.Amount);
        }
    }


}