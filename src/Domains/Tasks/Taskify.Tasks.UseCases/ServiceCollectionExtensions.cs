namespace Taskify.SharedKernel;

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
