namespace ShoppingBasket.Application.Interfaces.Repositories;

public interface IUnitOfWork : IDisposable
{
    Task<bool> CommitAsync(CancellationToken cancellationToken = default);
}