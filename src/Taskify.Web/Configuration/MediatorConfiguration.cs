namespace Taskify.Web.Configuration;
using MediatR;

using System.Reflection;

using Taskify.SharedKernel;
using Taskify.SharedKernel.Behaviour;
using Taskify.SharedKernel.Events;
using Taskify.Tasks.Core.ToDoItemAggregate;
using Taskify.Tasks.Infrastructure.Data;
using Taskify.Tasks.UseCases.ToDoItems.Create;

public static class MediatorConfiguration
{
    public static IServiceCollection AddMediatrConfiguration(this IServiceCollection services)
    {
        var mediatRAssemblies = new[]
      {
        Assembly.GetAssembly(typeof(ToDoItem)), // Core
        Assembly.GetAssembly(typeof(TasksDbContext)), // Infrastructure
        Assembly.GetAssembly(typeof(CreateToDoItemCommand)) // Use Cases
      };

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(mediatRAssemblies!))
                .AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>))
                .AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>))
                .AddScoped<IDomainEventDispatcher, MediatrDomainEventDispatcher>()
                .AddValidators();

        return services;
    }
}
