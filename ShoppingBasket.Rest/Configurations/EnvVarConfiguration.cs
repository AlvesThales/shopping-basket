using System.Reflection;

namespace ShoppingBasket.Configurations;

/// <summary>
/// Load AppSettings files, Environment Variables and secrets
/// </summary>
public static class EnvVarConfiguration
{
    internal static void ApplyDefaultEnvVarConfiguration(this IConfigurationBuilder builder, IWebHostEnvironment env, string[] args){
        
        builder.SetBasePath(env.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
            ;

        if (env.IsDevelopment())
        {
            var appAssembly = Assembly.Load(new AssemblyName(env.ApplicationName));
            builder.AddUserSecrets(appAssembly, optional: true, reloadOnChange: true);
        }

        builder.AddEnvironmentVariables();

        if (args is { Length: > 0 })
        {
            builder.AddCommandLine(args);
        }
        
    }
}