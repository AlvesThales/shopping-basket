using System.Reflection;
using Microsoft.EntityFrameworkCore;
using ShoppingBasket.Application;
using ShoppingBasket.Configurations;
using ShoppingBasket.Domain.Entities;
using ShoppingBasket.Infrastructure;
using OpenTelemetry.Metrics;
using Serilog;
using ShoppingBasket.Infrastructure.Persistence.Context;
using ShoppingBasket.Infrastructure.Persistence.Seed;

var builder = WebApplication.CreateBuilder(args);
var env = builder.Environment;


//Load AppSettings files and Environment Variables
builder.Configuration.ApplyDefaultEnvVarConfiguration(env, args);
builder.Host.UseSerilog(LoggingConfiguration.ConfigureLogger);

// Add services to the container.
builder.Services.AddControllers();

// Add Configuration of Swagger
builder.Services.ConfigureSwagger();

builder.Services.AddAuth();

builder.Services.AddHttpContextAccessor();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));


// Register Dependency Injection of all application
builder.Services
    .RegisterApplicationDependencyInjection()
    .RegisterInfrastructureDependencyInjection(builder.Configuration);

builder.Services.AddOpenTelemetry()
    .WithMetrics(x =>
    {
        x.AddPrometheusExporter();
        x.AddMeter("Microsoft.AspNetCore.Hosting",
            "Microsoft.AspNetCore.Server.Kestrel");
        x.AddView("http-server-request-duration",
            new ExplicitBucketHistogramConfiguration
            {
                Boundaries = new double[] { 0, 0.005, 0.01, 0.025, 0.05,
                    0.075, 0.1, 0.25, 0.5, 0.75, 1, 2.5, 5, 7.5, 10 }
            });
    });
    
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

var app = builder.Build();

// App configuration of swagger
app.AppSwaggerConfiguration();

app.MapPrometheusScrapingEndpoint();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.MapIdentityApi<Customer>();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        if (context.Database.GetPendingMigrations().Any())
        {
            context.Database.Migrate();
        }
        
        DbInitializer.Initialize(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating or seeding the database.");
    }
}


app.Run();