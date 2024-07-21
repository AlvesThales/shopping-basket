using Microsoft.EntityFrameworkCore;
using ShoppingBasket.Application.Interfaces.Repositories;
using ShoppingBasket.Domain.Common;
using ShoppingBasket.Infrastructure.Persistence.Context;

namespace ShoppingBasket.Infrastructure.Persistence.GenericRepository;

public class Repository<T>:IGenericRepository<T> where T : BaseEntity
{
        protected readonly ApplicationDbContext _dbContext;
        protected readonly DbSet<T> _dbSet;

        protected Repository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }
 
        public async Task<T> AddAsync(T entity, CancellationToken cancellationToken)
        {
            await _dbContext.Set<T>().AddAsync(entity, cancellationToken);
            return entity;
        }
        
        public Task Update(T entity)
        {
            _dbContext.Update(entity);
            return Task.CompletedTask;
        }
 
        public Task DeleteAsync(T entity, CancellationToken cancellationToken)
        {
            _dbContext.Set<T>().Remove(entity);
            return Task.CompletedTask;
        }
 
        public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _dbContext
                .Set<T>()
                .ToListAsync(cancellationToken: cancellationToken);
        }
 
        public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _dbContext.Set<T>().Where(x=>x.Id == id).SingleOrDefaultAsync(cancellationToken);
        }
}