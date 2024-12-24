namespace Taskify.Web.Configuration;

using FastEndpoints;

using Taskify.SharedKernel;
using Taskify.SharedKernel.Configuration;

public static class ServicesConfiguration
{
    public static IServiceCollection AddServices(
        this IServiceCollection services,
        IConfiguration configuration,
        ITaskifyEnvironmentSettings taskifyEnvironmentSettings,
        ILogger logger)
    {
        services.AddFastEndpoints()
                .AddTasksInfrastructure(configuration, taskifyEnvironmentSettings)
                .AddTasksUseCaseServices();
        logger.LogInformation("Services registered");
        return services;
    }
}
