using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShoppingBasket.Domain.Common;
using ShoppingBasket.Domain.Entities;

namespace ShoppingBasket.Infrastructure.Persistence.Context;

public class ApplicationDbContext : IdentityDbContext<Customer>
{

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    
    public DbSet<Basket> Baskets { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<StoredEvent> StoredEvents { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is AuditableEntity && (
                e.State == EntityState.Added
                || e.State == EntityState.Modified));
        
        foreach (var entityEntry in entries)
        {
            if (entityEntry.State == EntityState.Added)
            {
                ((AuditableEntity)entityEntry.Entity).CreatedAt = DateTime.Now;
                ((AuditableEntity)entityEntry.Entity).CreatedBy = "MyApp";
            }
            else
            {
                Entry((AuditableEntity)entityEntry.Entity).Property(p => p.CreatedAt).IsModified = false;
                Entry((AuditableEntity)entityEntry.Entity).Property(p => p.CreatedBy).IsModified = false;
            }
            ((AuditableEntity)entityEntry.Entity).ModifiedAt = DateTime.UtcNow;
            ((AuditableEntity)entityEntry.Entity).ModifiedBy = "MyApp";
        }
        
        return base.SaveChangesAsync(cancellationToken);
    }
}