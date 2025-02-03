namespace Taskify.Tasks.UseCases.Tests;

using MapsterMapper;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Taskify.Tasks.UseCases;
using Taskify.UnitTests.TestHelpers;

using Xunit;

public class ServiceCollectionExtensionsTests
{
    [Fact]
    public void AddTasksUseCaseServices_ShouldAddMapsterToServiceCollection()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services
            .AddScoped(typeof(ILogger<>), typeof(NullLogger<>))
            .AddTasksUseCaseServices();

        // Assert
        Assert.Contains(services, service => service.ServiceType == typeof(IMapper));
    }
}
