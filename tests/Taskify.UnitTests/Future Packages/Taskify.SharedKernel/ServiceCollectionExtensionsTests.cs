namespace Taskify.SharedKernel.Tests;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Taskify.Identity.Infrastructure.Data;
using Taskify.SharedKernel.Events;
using Taskify.UnitTests.TestHelpers;

using Xunit;

public class ServiceCollectionExtensionsTests
{
    [Fact]
    public void AddDefaultEventDispatcher_ShouldRegisterIDomainEventDispatcher()
    {
        // Arrange
        var services = new ServiceCollection();

        services
            .AddScoped(typeof(ILogger<>), typeof(NullLogger<>))
            .AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(IdentityDbContext)));

        // Act
        services.AddDefaultEventDispatcher();

        // Assert
        var serviceProvider = services.BuildServiceProvider();
        var dispatcher = serviceProvider.GetService<IDomainEventDispatcher>();

        Assert.NotNull(dispatcher);
        Assert.IsType<MediatrDomainEventDispatcher>(dispatcher);
    }
}
