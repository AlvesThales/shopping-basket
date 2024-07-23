using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Moq;
using ShoppingBasket.Application.Features.BasketFeature.CreateBasket;
using ShoppingBasket.Application.Interfaces;
using ShoppingBasket.Application.ViewModels.BasketItemViewModels;
using ShoppingBasket.Domain.Entities;
using ShoppingBasket.Tests.Mocks;
using Xunit;

namespace ShoppingBasket.Tests.Commands.BasketFeature.CreateBasket;

public class CreateBasketCommandTests : BasketFeatureTestsBase<CreateBasketCommand>
{
    private readonly Mock<IDiscountService> _discountServiceMock;
    private readonly CreateBasketCommandHandler _handler;
    private readonly Mock<ILogger<CreateBasketCommandHandler>> _loggerMock = new();
    private readonly Mock<UserManager<Customer>> _userManagerMock;

    public CreateBasketCommandTests()
    {
        _userManagerMock = UserManagerMock.CreateUserManagerMock();
        _discountServiceMock = new Mock<IDiscountService>();

        _handler = new CreateBasketCommandHandler(_loggerMock.Object, UnitOfWorkMock.Object, BusMock.Object,
            NotificationsMock.Object,
            BasketRepositoryMock.Object, ProductRepositoryMock.Object, _userManagerMock.Object,
            _discountServiceMock.Object);

        Command = new CreateBasketCommand("valid-customer-id", new List<CreateBasketItemInput>
        {
            new(Guid.NewGuid(), 1)
        });
    }

    [Fact]
    public async Task Should_Return_Basket_When_Succeeded()
    {
        var customerId = Guid.Parse("69f85c32-4cca-40a0-8d27-327986e5cfcd");
        var customer = new Customer { Id = customerId.ToString() };
        var productId = Guid.Parse("ef6d5919-82a2-4f4d-908c-5eb696d6a3f9");

        var expectedResult = new Basket(customer, new List<BasketItem>
        {
            new(new Product { Id = productId, Price = 10m }, 10m, 1)
        }, false, false);

        // Arrange
        UnitOfWorkMock.MockCommit();
        _userManagerMock.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(customer);
        ProductRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Product { Id = productId, Price = 10m });
        BasketRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Basket>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Basket basket, CancellationToken cancellationToken) => basket);
        _discountServiceMock.Setup(x => x.ApplyDiscounts(It.IsAny<Basket>())).Callback<Basket>(_ => { });

        // Act
        var handlerResponse = await _handler.Handle(Command, new CancellationToken());

        // Assert
        Assert.True(handlerResponse.IsSuccess);
        Assert.Equivalent(expectedResult, handlerResponse.GetValue());
    }
}