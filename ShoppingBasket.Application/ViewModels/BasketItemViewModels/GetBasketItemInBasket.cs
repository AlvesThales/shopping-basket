using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingBasket.Application.ViewModels.BasketItemViewModels;

public class GetBasketItemInBasket
{
    public Guid Id { get; set; }
    public string ProductName { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }
    public int Amount { get; set; }
    public decimal TotalDiscount { get; set; }

    [NotMapped]
    public decimal OriginalPrice => UnitPrice * Amount;
    
    [NotMapped]
    public decimal DiscountedPrice => OriginalPrice - TotalDiscount;

    public GetBasketItemInBasket()
    {
    }
}