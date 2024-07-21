using Riok.Mapperly.Abstractions;
using ShoppingBasket.Application.ViewModels.OrderItemViewModels;
using ShoppingBasket.Application.ViewModels.OrderViewModels;
using ShoppingBasket.Domain.Entities;

namespace ShoppingBasket.Application.Mappings;

[Mapper]
public partial class OrderItemMapper
{
    public partial CreateOrderItemOutput OrderItemToCreateOrderItemOutput(Order orderItem);
    public partial GetOrderItemInOrder OrderItemToOrderItemOutput(OrderItem orderItem);

}