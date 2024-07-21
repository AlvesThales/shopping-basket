using Microsoft.AspNetCore.Identity;

namespace ShoppingBasket.Domain.Entities;

public class Customer : IdentityUser
{
    public virtual ICollection<Basket> Orders { get; set; }

    public Customer()
    {
    }

    public Customer(string email, ICollection<Basket> orders)
    {
        Email = email;
        Orders = orders;
    }
}