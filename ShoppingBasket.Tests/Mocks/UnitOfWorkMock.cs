using Moq;
using ShoppingBasket.Application.Interfaces.Repositories;

namespace ShoppingBasket.Tests.Mocks;

public static class UnitOfWorkMock
{
    public static void MockCommit(this Mock<IUnitOfWork> uow, bool isSuccess = true)
    {
        uow
            .Setup(x => x.CommitAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(isSuccess);
    }
}