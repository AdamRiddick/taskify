namespace Taskify.Tasks.UseCases;

using Mapster;

using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTasksUseCaseServices(
        this IServiceCollection services)
    {
        services.AddMapster();
        return services;
    }
}
