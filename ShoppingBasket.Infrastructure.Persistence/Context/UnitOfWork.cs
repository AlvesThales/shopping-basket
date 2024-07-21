using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShoppingBasket.Application.Interfaces.Repositories;

namespace ShoppingBasket.Infrastructure.Persistence.Context;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<UnitOfWork> _logger;

    public UnitOfWork(ApplicationDbContext context, ILogger<UnitOfWork> logger)
    {
        _logger = logger;
        _context = context;
    }
    public void Dispose()
    {
        _context.Dispose();
    }

    public async Task<bool> CommitAsync(CancellationToken cancellationToken)
    {
        try
        {
            Validate();
            var n = await _context.SaveChangesAsync(cancellationToken);
            return n > 0;
        }
        catch (ValidationException ex)
        {
            _logger.LogError("{CommitAsyncName} Db validation error with code {ExceptionCode}: {InnerExceptionMessage}", nameof(CommitAsync),ex?.InnerException?.Data["SqlState"],ex?.InnerException?.Data["MessageText"]);
            return false;
        }
        catch (DbUpdateException ex)
        {
           _logger.LogError("{CommitAsyncName} Db update error with code {ExceptionCode}: {InnerExceptionMessage}", nameof(CommitAsync),ex?.InnerException?.Data["SqlState"],ex?.InnerException?.Data["MessageText"]);
            return false;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{CommitAsyncName} exception: {InnerExceptionMessage}", nameof(CommitAsync),
                ex?.InnerException?.Message);
            return false;
        }
    }
    
    private void Validate()
    {
        var entities = _context.ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified)
            .Select(e => e.Entity)
            .ToList();

        foreach (var entity in entities)
        {
            var validationContext = new ValidationContext(entity);
            Validator.ValidateObject(entity, validationContext, validateAllProperties: true);
        }
    }

}