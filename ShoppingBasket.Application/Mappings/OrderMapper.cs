using Riok.Mapperly.Abstractions;
using ShoppingBasket.Application.ViewModels.OrderViewModels;
using ShoppingBasket.Domain.Entities;

namespace ShoppingBasket.Application.Mappings;

[Mapper]
public partial class OrderMapper
{
    public partial CreateOrderOutput OrderToCreateOrderOutput(Order order);
    public partial UpdateOrderOutput OrderToUpdateOrderOutput(Order order);
    public partial GetOrderSimple OrderToOrderOutput(Order order);
}