using Microsoft.AspNetCore.Identity;
using Moq;
using ShoppingBasket.Application.Interfaces.Repositories;
using ShoppingBasket.Domain.Entities;
using ShoppingBasket.Tests.Base;

namespace ShoppingBasket.Tests.Commands.BasketFeature.CreateBasket;

public abstract class BasketFeatureTestsBase<TCommand> : CommandTestBase<TCommand>
{
    protected readonly Mock<IBasketRepository> BasketRepositoryMock = new();
    protected readonly Mock<IProductRepository> ProductRepositoryMock = new();
}