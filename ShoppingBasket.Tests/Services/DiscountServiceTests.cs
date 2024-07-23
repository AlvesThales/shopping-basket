using ShoppingBasket.Application.Services;
using ShoppingBasket.Domain.Entities;
using Xunit;

namespace ShoppingBasket.Tests.Services;

public class DiscountServiceTests
{
    private readonly DiscountService _discountService;

    public DiscountServiceTests()
    {
        _discountService = new DiscountService();
    }

    [Fact]
    public void ApplyDiscounts_ShouldApplyPercentageDiscount()
    {
        // Arrange
        var customer = new Customer { Id = Guid.NewGuid().ToString() };
        var product = new Product { Id = Guid.NewGuid(), Name = "Apples", Price = 10m };
        var basketItems = new List<BasketItem>
        {
            new BasketItem(product, product.Price, 5)
        };
        var basket = new Basket(customer, basketItems, false, false);

        // Act
        _discountService.ApplyDiscounts(basket);

        // Assert
        Assert.Equal(45m, basket.TotalBasketDiscountedPrice);
    }

    [Fact]
    public void ApplyDiscounts_ShouldApplyMultiBuyDiscount()
    {
        // Arrange
        var customer = new Customer { Id = Guid.NewGuid().ToString() };
        var soupProduct = new Product { Id = Guid.NewGuid(), Name = "Soup", Price = 5m };
        var breadProduct = new Product { Id = Guid.NewGuid(), Name = "Bread", Price = 2m };
        var basketItems = new List<BasketItem>
        {
            new BasketItem(soupProduct, soupProduct.Price, 2),
            new BasketItem(breadProduct, breadProduct.Price, 1)
        };
        var basket = new Basket(customer, basketItems, false, false);

        // Act
        _discountService.ApplyDiscounts(basket);

        // Assert
        Assert.Equal(11, basket.TotalBasketDiscountedPrice);
    }
}