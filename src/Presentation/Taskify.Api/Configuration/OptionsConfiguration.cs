namespace Taskify.Api.Configuration;

public static class OptionsConfiguration
{
    public static IServiceCollection AddOptionConfigs(this IServiceCollection services,
                                                    ILogger logger)
    {
        logger.LogInformation("Options configured.");
        return services;
    }
}