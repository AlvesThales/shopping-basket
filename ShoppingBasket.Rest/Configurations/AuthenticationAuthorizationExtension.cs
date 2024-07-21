using Microsoft.AspNetCore.Identity;
using ShoppingBasket.Domain.Entities;
using ShoppingBasket.Infrastructure.Persistence.Context;

namespace ShoppingBasket.Configurations;

public static class AuthenticationAuthorizationExtension
{
    public static void AddAuth(this IServiceCollection services)
    {
        services.AddAuthentication().AddBearerToken(IdentityConstants.BearerScheme);
        services.AddAuthorizationBuilder();
        services.AddIdentityCore<Customer>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddApiEndpoints();
    }
}