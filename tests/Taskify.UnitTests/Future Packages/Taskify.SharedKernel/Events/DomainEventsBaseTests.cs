namespace Taskify.SharedKernel.Events.Tests;

using System;

using Taskify.UnitTests.TestHelpers.Objects;

using Xunit;

public class DomainEventBaseTests
{
    [Fact]
    public void DateOccurred_ShouldBeSetToUtcNow()
    {
        // Arrange
        var domainEvent = new TestDomainEvent();

        // Act
        var dateOccurred = domainEvent.DateOccurred;

        // Assert
        Assert.Equal(DateTime.UtcNow, dateOccurred, TimeSpan.FromSeconds(1));
    }
}
