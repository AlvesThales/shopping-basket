using ShoppingBasket.Domain.Common;

namespace ShoppingBasket.Domain.Entities;

public class Product : AuditableEntity
{
    public virtual ICollection<BasketItem> OrderItem { get; set; } = new List<BasketItem>();
    public string Name { get; set; }
    public Decimal Price { get; set; }

    public Product()
    {
    }

    public Product(string name, Decimal price)
    {
        Name = name;
        Price = price;
    }
}