using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;

namespace MongoDb.Application.Configurations
{
    public static class LogConfiguration
    {
        public static void ConfigureLogging(IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .Enrich.WithExceptionDetails()
                .Enrich.WithCorrelationId()
                .WriteTo.Debug()
                .WriteTo.Console()
                .WriteTo.Elasticsearch(ConfigureElastickSink(configuration))
                .Enrich.WithProperty("Environment", configuration.GetValue<string>("ASPNETCORE_ENVIRONMENT"))
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
        }

        private static ElasticsearchSinkOptions ConfigureElastickSink(IConfiguration configuration)
        {
            return new ElasticsearchSinkOptions(new Uri(configuration.GetValue<string>("ElasticConfigurationUri")))
            {
                AutoRegisterTemplate = true,
                IndexFormat = $"bookstore-{DateTime.UtcNow:yyyy-MM}"
            };
        }
    }
}
