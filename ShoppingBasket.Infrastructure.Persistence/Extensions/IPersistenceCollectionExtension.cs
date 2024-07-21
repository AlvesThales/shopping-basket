using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ShoppingBasket.Application.Interfaces.Repositories;
using ShoppingBasket.Infrastructure.Persistence.Context;
using ShoppingBasket.Infrastructure.Persistence.GenericRepository;

namespace ShoppingBasket.Infrastructure.Persistence.Extensions;

internal static class PersistenceCollectionExtension
{
    public static void AddPersistenceLayer(this IServiceCollection services, IConfiguration configuration)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);

        
        services
            .AddDbContext(configuration)
            .AddUnitOfWork()
            .AddRepositories();
    }


    private static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString,
                builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        return services;
    }

    private static IServiceCollection AddUnitOfWork(this IServiceCollection services)
    {
        return services
            .AddScoped<IUnitOfWork, UnitOfWork>()
            .AddScoped<IEventStore, EventStore>();
    }
}