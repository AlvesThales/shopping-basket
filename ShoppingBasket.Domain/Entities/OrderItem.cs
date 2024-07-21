﻿using System.ComponentModel.DataAnnotations.Schema;
using ShoppingBasket.Domain.Common;

namespace ShoppingBasket.Domain.Entities;

public class OrderItem : AuditableEntity
{
    public Order Order { get; set; } = null;
    public Guid OrderId { get; set; }
    public Product Product { get; set; }
    public Guid ProductId { get; set; }
    public decimal UnitPrice { get; set; }
    public int Amount { get; set; }

    public OrderItem()
    {
    }

    public OrderItem(Product product, decimal price, int amount)
    {
        Product = product;
        UnitPrice = price;
        Amount = amount;
    }
}