namespace ShoppingBasket.Application.ViewModels.BasketItemViewModels;

public class GetBasketItemInBasket
{
    public Guid Id { get; set; }
    public string ProductName { get; set; }
    public decimal UnitPrice { get; set; }
    public int Amount { get; set; }

    public GetBasketItemInBasket()
    {
    }
}
    