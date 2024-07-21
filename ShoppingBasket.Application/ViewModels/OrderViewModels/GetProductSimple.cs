using ShoppingBasket.Domain.Entities;

namespace ShoppingBasket.Application.ViewModels.ProductViewModels;

public record GetProductSimple(Guid Id, string Name, Decimal Price);