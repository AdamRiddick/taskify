﻿namespace Taskify.Web.Configuration;

using FastEndpoints;

using Taskify.Identity.Infrastructure;
using Taskify.Identity.UseCases;
using Taskify.SharedKernel.Configuration;
using Taskify.Tasks.Infrastructure;
using Taskify.Tasks.UseCases;

public static class ServicesConfiguration
{
    public static IServiceCollection AddServices(
        this IServiceCollection services,
        IConfiguration configuration,
        ITaskifyEnvironmentSettings taskifyEnvironmentSettings,
        ILogger logger)
    {
        services.AddFastEndpoints()
                .AddIdentityInfrastructure(configuration, taskifyEnvironmentSettings)
                .AddTasksInfrastructure(configuration, taskifyEnvironmentSettings)
                .AddIdentityUseCaseServices()
                .AddTasksUseCaseServices();
        logger.LogInformation("Services registered");
        return services;
    }
}
