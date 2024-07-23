using ShoppingBasket.Domain.Common;

namespace ShoppingBasket.Domain.Entities;

public class Basket : AuditableEntity
{
    public virtual Customer Customer { get; set; }
    public virtual ICollection<BasketItem> BasketItems { get; set; } = new List<BasketItem>();
    public bool IsPaid { get; set; }
    public decimal PaidAmount { get; set; }
    public bool IsDeleted { get; set; }
    public decimal TotalBasketOriginalPrice
    {
        get { return BasketItems.Sum(b => b.OriginalPrice); }
    }
    public decimal TotalBasketDiscountedPrice
    {
        get { return BasketItems.Sum(b => b.DiscountedPrice); }
    }   

    public Basket()
    {
    }

    public Basket(Customer customer, ICollection<BasketItem> basketItems, bool isPaid, bool isDeleted)
    {
        Customer = customer;
        BasketItems = basketItems;
        IsPaid = isPaid;
        IsDeleted = isDeleted;
    }

    public void UpdateBasketItems(ICollection<BasketItem> basketItems)
    {
        if (IsPaid)
        {
            throw new InvalidOperationException("BasketAlreadyPaid");
        }

        BasketItems.Clear();

        foreach (var item in basketItems)
        {
            BasketItems.Add(item);
        }
    }

    public void Pay()
    {
        PaidAmount = TotalBasketDiscountedPrice;
        IsPaid = true;
    }
    
    public void ApplyDiscounts(IEnumerable<Discount> discounts)
    {
        foreach (var discount in discounts)
        {
            discount.ApplyDiscount(this);
        }
    }
}