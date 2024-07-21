using System.ComponentModel.DataAnnotations.Schema;
using ShoppingBasket.Domain.Common;

namespace ShoppingBasket.Domain.Entities;

public class BasketItem : AuditableEntity
{
    public Basket Basket { get; set; } = null;
    public Guid BasketId { get; set; }
    public Product Product { get; set; }
    public Guid ProductId { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }
    public int Amount { get; set; }

    public BasketItem()
    {
    }

    public BasketItem(Product product, decimal price, int amount)
    {
        Product = product;
        UnitPrice = price;
        Amount = amount;
    }
}