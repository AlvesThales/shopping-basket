using ShoppingBasket.Domain.Common;

namespace ShoppingBasket.Domain.Entities;

public class Product : AuditableEntity
{
    public virtual ICollection<OrderItem> OrderItem { get; set; } = new List<OrderItem>();
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