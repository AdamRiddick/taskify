﻿namespace Taskify.Tasks.Infrastructure;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Taskify.Tasks.Infrastructure.Data;
using Taskify.SharedKernel.Data;

public static class ServiceCollectionExtensions
{
#pragma warning disable S125,IDE0060 // Sections of code should not be commented out
    public static IServiceCollection AddTasksInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {

        //var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<TasksDbContext>(options =>
                    options.UseInMemoryDatabase("Taskify"));
        //options.UseSqlServer(connectionString));
#pragma warning restore S125 // Sections of code should not be commented out

        return services
                .RegisterRepositories();
    }

    private static IServiceCollection RegisterRepositories(
        this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(TasksRepository<>));
        services.AddScoped(typeof(IReadRepository<>), typeof(TasksRepository<>));
        return services;
    }
}
