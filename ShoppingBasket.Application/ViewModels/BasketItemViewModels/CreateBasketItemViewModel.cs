namespace ShoppingBasket.Application.ViewModels.BasketItemViewModels;

public record CreateBasketItemInput(Guid ProductId, int Amount);

public class CreateBasketItemOutput
{
    public Guid ProductId { get; set; }
    public string ProductName {  get; set; } 
    public decimal UnitPrice { get; set; }
    public int Amount { get; set; }
    
    public CreateBasketItemOutput()
    {
    }
}