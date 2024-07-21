using Microsoft.AspNetCore.Identity;

namespace ShoppingBasket.Domain.Entities;

public class Customer : IdentityUser
{
    public virtual ICollection<Basket> Baskets { get; set; }

    public Customer()
    {
    }

    public Customer(string email, ICollection<Basket> baskets)
    {
        Email = email;
        Baskets = baskets;
    }
}