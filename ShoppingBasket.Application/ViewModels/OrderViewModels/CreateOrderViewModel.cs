using ShoppingBasket.Domain.Entities;

namespace ShoppingBasket.Application.ViewModels.OrderViewModels;

public record CreateOrderInput(Guid CustomerId, ICollection<CreateOrderItemInput> OrderItems);
public record UpdateOrderInput(Guid OrderId, ICollection<CreateOrderItemInput> OrderItems);
public record CreateOrderOutput(string Id, string CustomerId, ICollection<CreateOrderItemOutput> OrderItems, decimal TotalPrice, bool IsPaid, bool IsDeleted);
public record UpdateOrderOutput(string Id, string CustomerId, ICollection<CreateOrderItemOutput> OrderItems, decimal TotalPrice, bool IsPaid, bool IsDeleted);