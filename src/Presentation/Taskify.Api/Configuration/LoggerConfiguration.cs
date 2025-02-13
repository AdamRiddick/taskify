namespace Taskify.Api.Configuration;

using Serilog;
using Serilog.Extensions.Logging;

public static class LoggerConfiguration
{
    public static ILogger<ITaskify> AddLoggerConfiguration(
        this WebApplicationBuilder builder)
    {
        var logger = Log.Logger = new Serilog.LoggerConfiguration()
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateLogger();

        builder.Host.UseSerilog((_, config) => config.ReadFrom.Configuration(builder.Configuration));

        return new SerilogLoggerFactory(logger).CreateLogger<ITaskify>();
    }
}
