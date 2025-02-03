namespace Taskify.SharedKernel.Configuration.Tests;

using Xunit;
public class TaskifyEnvironmentSettingsTests
{
    [Fact]
    public void IsDevelopment_WhenEnvironmentNameIsDevelopment_ReturnsTrue()
    {
        // Arrange
        var environmentSettings = new TaskifyEnvironmentSettings("Development");

        // Act
        var isDevelopment = environmentSettings.IsDevelopment;

        // Assert
        Assert.True(isDevelopment);
    }

    [Fact]
    public void IsDevelopment_WhenEnvironmentNameIsNotDevelopment_ReturnsFalse()
    {
        // Arrange
        var environmentSettings = new TaskifyEnvironmentSettings("Production");

        // Act
        var isDevelopment = environmentSettings.IsDevelopment;

        // Assert
        Assert.False(isDevelopment);
    }
}
