using ShoppingBasket.Domain.Entities;

namespace ShoppingBasket.Application.ViewModels.OrderViewModels;

public record CreateOrderItemInput(Guid ProductId, int Amount);

public class CreateOrderItemOutput
{
    public Guid ProductId { get; set; }
    public string ProductName {  get; set; } 
    public decimal UnitPrice { get; set; }
    public int Amount { get; set; }


    public CreateOrderItemOutput()
    {
    }
}