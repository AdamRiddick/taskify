namespace Taskify.Api.Configuration;

using Taskify.SharedKernel.Configuration;

public static class SettingsConfiguration
{
    public static ITaskifyEnvironmentSettings AddSettings(
                this IServiceCollection services,
                IWebHostEnvironment environment,
                ILogger logger)
    {
        var taskifyEnvironmentSettings = new TaskifyEnvironmentSettings(environment.EnvironmentName);
        services.AddSingleton<ITaskifyEnvironmentSettings>(taskifyEnvironmentSettings);
        logger.LogInformation("Settings registered");
        return taskifyEnvironmentSettings;
    }
}
