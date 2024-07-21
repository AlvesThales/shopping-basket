using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;

namespace ShoppingBasket.Configurations;

public static class LoggingConfiguration
{
    public static Action<HostBuilderContext, LoggerConfiguration> ConfigureLogger =>
        (hostingContext, loggerConfiguration) =>
        {
            var env = hostingContext.HostingEnvironment;
            loggerConfiguration
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails()
                .Enrich.WithProperty("Application", env.ApplicationName)
                .Enrich.WithProperty("Environment", env.EnvironmentName)
                .WriteTo.Debug() // FOR DEBUG PURPOSES
                .WriteTo.Console(outputTemplate: "{Timestamp:yyyy:MM:dd HH:mm:ss.fff zzz} [{Level}] [{SourceContext}] {Message}{NewLine}{Exception}");

            loggerConfiguration.Destructure.With(new MaxDepthDestructuringPolicy(100));

            var elasticUrl = hostingContext.Configuration.GetValue<string>("ElasticSearch:Uri");
            var indexName = hostingContext.Configuration.GetValue<string>("Serilog:Elastic:IndexName");
            var environment = hostingContext.HostingEnvironment.EnvironmentName;
            if (!string.IsNullOrEmpty(elasticUrl))
            {
                loggerConfiguration.WriteTo.Elasticsearch(
                    new ElasticsearchSinkOptions(new Uri(elasticUrl))
                    {
                        AutoRegisterTemplate = true,
                        AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv7,
                        IndexFormat = indexName+"-logs-{0:yyyy.MM.dd}-" + environment,
                        MinimumLogEventLevel = LogEventLevel.Debug,
                        TypeName = null,
                    });
            }
            else
            {
               Console.WriteLine("Elastic URI not configured.");
            }

            loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration);
        };
}

public class MaxDepthDestructuringPolicy(int maxDepth) : IDestructuringPolicy
{
    public bool TryDestructure(object value, ILogEventPropertyValueFactory propertyValueFactory, out LogEventPropertyValue? result)
    {
        if (value is Exception exception)
        {
            result = DestructureException(exception, 0);
            return true;
        }

        result = null;
        return false;
    }

    private LogEventPropertyValue DestructureException(Exception exception, int depth)
    {
        if (depth > maxDepth)
        {
            return new ScalarValue(exception.ToString());
        }
  
        var properties = new List<LogEventProperty>();
        foreach (var propertyInfo in exception.GetType().GetProperties())
        {
            if (propertyInfo.GetIndexParameters().Length != 0) continue;
            var value = propertyInfo.GetValue(exception, null);
            if (value == null) continue;
            var destructuredValue = DestructureException(value as Exception, depth + 1);
            properties.Add(new LogEventProperty(propertyInfo.Name, destructuredValue));
        }

        return new StructureValue(properties);
    }
}