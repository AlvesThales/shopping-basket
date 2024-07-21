using ShoppingBasket.Domain.Entities;

namespace ShoppingBasket.Application.ViewModels.ProductViewModels;

public record CreateProductInput(string Name, Decimal Price);

public record CreateProductOutput(Guid Id, string Name, Decimal Price);