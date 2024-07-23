using ShoppingBasket.Domain.Entities;
using ShoppingBasket.Infrastructure.Persistence.Context;

namespace ShoppingBasket.Infrastructure.Persistence.Seed;

using System.Linq;

public static class DbInitializer
{
    public static void Initialize(ApplicationDbContext context)
    {
        if (context.Products.Any())
        {
            return;
        }

        var products = new[]
        {
            new Product { Name = "Soup", Price = 0.65m },
            new Product { Name = "Bread", Price = 0.80m },
            new Product { Name = "Milk", Price = 1.30m },
            new Product { Name = "Apples", Price = 1.00m }
        };

        foreach (var product in products)
        {
            context.Products.Add(product);
        }

        context.SaveChanges();
    }
}
