using Microsoft.AspNetCore.Identity;

namespace ShoppingBasket.Domain.Entities;

public class Customer : IdentityUser
{
    public virtual ICollection<Order> Orders { get; set; }

    public Customer()
    {
    }

    public Customer(string email, ICollection<Order> orders)
    {
        Email = email;
        Orders = orders;
    }
}