using System.ComponentModel.DataAnnotations.Schema;
using ShoppingBasket.Domain.Common;

namespace ShoppingBasket.Domain.Entities;

public class Order : AuditableEntity
{
    public virtual Customer Customer { get; set; }
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    public bool IsPaid { get; set; }
    public decimal PaidAmount { get; set; }
    public bool IsDeleted { get; set; }

    [NotMapped]
    public decimal TotalPrice
    {
        get { return OrderItems.Sum(o => o.UnitPrice * o.Amount); }
    }

    public Order()
    {
    }

    public Order(Customer customer, ICollection<OrderItem> orderItems, bool isPaid, bool isDeleted)
    {
        Customer = customer;
        OrderItems = orderItems;
        IsPaid = isPaid;
        IsDeleted = isDeleted;
    }

    public void UpdateOrderItems(ICollection<OrderItem> orderItems)
    {
        if (IsPaid)
        {
            throw new InvalidOperationException("OrderAlreadyPaid");
        }

        OrderItems.Clear();

        foreach (var item in orderItems)
        {
            OrderItems.Add(item);
        }
    }

    public void Pay()
    {
        PaidAmount = TotalPrice;
        IsPaid = true;
    }
}