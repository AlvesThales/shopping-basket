using Microsoft.Extensions.Configuration;

namespace ShoppingBasket.Tests.Configuration;

public static class Configuration
{
    // Add here settings to use
    private static readonly IReadOnlyList<KeyValuePair<string, string?>> Settings =
        new List<KeyValuePair<string, string?>>();

    public static IConfigurationRoot BuildEmptyConfiguration(this IConfigurationBuilder builder)
    {
        return builder
            .AddInMemoryCollection(Array.Empty<KeyValuePair<string, string?>>())
            .Build();
    }

    public static IConfigurationRoot BuildConfiguration(this IConfigurationBuilder builder)
    {
        return builder
            .AddInMemoryCollection(Settings)
            .Build();
    }
}