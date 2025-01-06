namespace Taskify.Identity.UseCases;

using Mapster;

using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddIdentityUseCaseServices(
        this IServiceCollection services)
    {
        services.AddMapster();
        return services;
    }
}
