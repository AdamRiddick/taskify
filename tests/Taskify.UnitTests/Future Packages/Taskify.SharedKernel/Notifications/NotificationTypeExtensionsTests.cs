namespace Taskify.SharedKernel.Notifications.Tests;

using Xunit;

public class NotificationTypeExtensionsTests
{
    [Fact]
    public void HasFlag_ShouldReturnTrue_WhenValueHasFlag()
    {
        // Arrange
        var value = NotificationType.Marketing;
        var flag = NotificationType.Marketing;

        // Act
        var result = value.HasFlag(flag);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void HasFlag_ShouldReturnFalse_WhenValueDoesNotHaveFlag()
    {
        // Arrange
        var value = NotificationType.Marketing;
        var flag = NotificationType.Security;

        // Act
        var result = value.HasFlag(flag);

        // Assert
        Assert.False(result);
    }
}
