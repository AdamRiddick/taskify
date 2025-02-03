namespace Taskify.UnitTests.Future_Packages.Taskify.Infrastructure.Notifications.Dispatcher;

using global::Taskify.Infrastructure.Notifications.Dispatcher.Data;
using global::Taskify.Infrastructure.Notifications.Dispatcher;
using global::Taskify.SharedKernel.Data;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Xunit;
using global::Taskify.Tasks.Infrastructure.Data;
using Microsoft.Extensions.Logging;
using global::Taskify.SharedKernel;
using global::Taskify.UnitTests.TestHelpers.Objects;
using global::Taskify.UnitTests.TestHelpers;

public class ServiceCollectionExtensionsTests
{
    [Fact]
    public void AddNotificationDispatcherInfrastructure_ShouldRegisterDbContext()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddScoped(typeof(ILogger<>), typeof(NullLogger<>))
                .AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(TasksDbContext)))
                .AddDefaultEventDispatcher();

        var configuration = new ConfigurationBuilder().Build(); // Empty config

        // Act
        services.AddNotificationDispatcherInfrastructure(configuration);
        var provider = services.BuildServiceProvider();

        // Assert
        var dbContext = provider.GetService<NotificationsDbContext>();
        Assert.NotNull(dbContext);
        Assert.IsType<NotificationsDbContext>(dbContext);
    }

    [Fact]
    public void AddNotificationDispatcherInfrastructure_ShouldUseInMemoryDatabase()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddScoped(typeof(ILogger<>), typeof(NullLogger<>))
                .AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(TasksDbContext)))
                .AddDefaultEventDispatcher();
        var configuration = new ConfigurationBuilder().Build();

        // Act
        services.AddNotificationDispatcherInfrastructure(configuration);
        var provider = services.BuildServiceProvider();

        // Assert
        var dbContext = provider.GetRequiredService<NotificationsDbContext>();
        var options = dbContext.Database.ProviderName;

        Assert.Equal("Microsoft.EntityFrameworkCore.InMemory", options);
    }

    [Fact]
    public void AddNotificationDispatcherInfrastructure_ShouldRegisterRepositories()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddScoped(typeof(ILogger<>), typeof(NullLogger<>))
                .AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(TasksDbContext)))
                .AddDefaultEventDispatcher();
        var configuration = new ConfigurationBuilder().Build();

        // Act
        services.AddNotificationDispatcherInfrastructure(configuration);
        var provider = services.BuildServiceProvider();

        // Assert
        var repository = provider.GetService<IRepository<TestEntity>>();
        Assert.NotNull(repository);
        Assert.IsType<NotificationsRepository<TestEntity>>(repository);
    }
}