namespace Taskify.Tasks.UseCases.Tests;

using MapsterMapper;

using Microsoft.Extensions.DependencyInjection;

using Taskify.Tasks.UseCases;

using Xunit;

public class ServiceCollectionExtensionsTests
{
    [Fact]
    public void AddTasksUseCaseServices_ShouldAddMapsterToServiceCollection()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddTasksUseCaseServices();

        // Assert
        Assert.Contains(services, service => service.ServiceType == typeof(IMapper));
    }
}
