namespace Taskify.SharedKernel;

using FluentValidation;

using Microsoft.Extensions.DependencyInjection;

using System.Reflection;

using Taskify.SharedKernel.Events;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDefaultEventDispatcher(
        this IServiceCollection services)
    {
        services.AddScoped<IDomainEventDispatcher, MediatrDomainEventDispatcher>();
        return services;
    }

    public static IServiceCollection AddValidators(
        this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        return services;
    }
}
