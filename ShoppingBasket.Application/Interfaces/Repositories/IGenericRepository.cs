using ShoppingBasket.Domain.Common;

namespace ShoppingBasket.Application.Interfaces.Repositories;

public interface IGenericRepository<T> where T: BaseEntity
{
    Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);
    Task Update(T entity);
    Task DeleteAsync(T entity, CancellationToken cancellationToken = default);
    Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}