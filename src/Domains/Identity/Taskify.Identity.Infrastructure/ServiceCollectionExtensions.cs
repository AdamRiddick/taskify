namespace Taskify.Identity.Infrastructure;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Taskify.SharedKernel.Configuration;
using Taskify.SharedKernel.Data;
using Taskify.Identity.Infrastructure.Data;

public static class ServiceCollectionExtensions
{
#pragma warning disable S125,IDE0060 // Sections of code should not be commented out
    public static IServiceCollection AddIdentityInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration, 
        ITaskifyEnvironmentSettings taskifyEnvironmentSettings)
    {

        //var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<IdentityDbContext>(options =>
                    options.UseInMemoryDatabase("Taskify"));
        //options.UseSqlServer(connectionString));
#pragma warning restore S125 // Sections of code should not be commented out

        return services
                .RegisterRepositories();
    }

    private static IServiceCollection RegisterRepositories(
        this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(IdentityRepository<>));
        services.AddScoped(typeof(IReadRepository<>), typeof(IdentityRepository<>));
        return services;
    }
}
