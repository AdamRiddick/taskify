namespace Taskify.Identity.UseCases.Tests;

using MapsterMapper;

using Microsoft.Extensions.DependencyInjection;

using Xunit;

public class ServiceCollectionExtensionsTests
{
    [Fact]
    public void AddIdentityUseCaseServices_ShouldAddMapster()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddIdentityUseCaseServices();

        // Assert
        Assert.Contains(services, service =>
            service.ServiceType == typeof(IMapper) &&
            service.ImplementationType == typeof(Mapper));
    }
}
