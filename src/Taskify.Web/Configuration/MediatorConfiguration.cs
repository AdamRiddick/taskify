namespace Taskify.Web.Configuration;

using MediatR;

using System.Reflection;

using Taskify.Identity.Core.ContextTypeAggregate;
using Taskify.Identity.Infrastructure.Data;
using Taskify.Identity.UseCases.ContextTypes.Create;
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
        Assembly.GetAssembly(typeof(ContextType)), // Identity.Core
        Assembly.GetAssembly(typeof(IdentityDbContext)), // Identity.Infrastructure
        Assembly.GetAssembly(typeof(CreateContextTypeCommand)), // Identity.UseCases
        Assembly.GetAssembly(typeof(ToDoItem)), // Tasks.Core
        Assembly.GetAssembly(typeof(TasksDbContext)), // Tasks.Infrastructure
        Assembly.GetAssembly(typeof(CreateToDoItemCommand)) // Tasks.UseCases
      };

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(mediatRAssemblies!))
                .AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>))
                .AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>))
                .AddScoped<IDomainEventDispatcher, MediatrDomainEventDispatcher>()
                .AddValidators();

        return services;
    }
}
