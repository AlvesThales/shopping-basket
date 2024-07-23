using Microsoft.Extensions.Configuration;
using Moq;
using ShoppingBasket.Application.Interfaces.Repositories;
using ShoppingBasket.Domain.Common;
using ShoppingBasket.Domain.Common.Interfaces;
using ShoppingBasket.Tests.Configuration;

namespace ShoppingBasket.Tests.Base;

public abstract class CommandTestBase<TCommand>
{
    #region Mocks

    protected readonly Mock<IMediatorHandler> BusMock = new();
    protected readonly Mock<IUnitOfWork> UnitOfWorkMock = new();
    protected readonly Mock<DomainNotificationHandler> NotificationsMock = new();

    #endregion
    
    protected readonly IConfigurationRoot Configuration = new ConfigurationBuilder().BuildConfiguration();
    protected TCommand Command = default!;
}