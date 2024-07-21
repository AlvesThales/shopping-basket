using ShoppingBasket.Domain.Entities;
using ShoppingBasket.Application.ViewModels.OrderItemViewModels;

namespace ShoppingBasket.Application.ViewModels.OrderViewModels;

public record GetOrderSimple(Guid Id, Guid CustomerId, ICollection<GetOrderItemInOrder> OrderItems, decimal TotalPrice, bool IsPaid, bool IsDeleted);