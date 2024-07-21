namespace ShoppingBasket.Application.ViewModels.OrderItemViewModels;

public class GetOrderItemInOrder
{
    public Guid Id { get; set; }
    public string ProductName { get; set; }
    public decimal UnitPrice { get; set; }
    public int Amount { get; set; }

    public GetOrderItemInOrder()
    {
    }
}
    