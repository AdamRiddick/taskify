namespace Taskify.SharedKernel.Events.Tests;

using Taskify.UnitTests.TestHelpers.Objects;

using Xunit;

public class HasDomainEventsBaseTests
{
    [Fact]
    public void RegisterDomainEvent_ShouldAddEventToList()
    {
        // Arrange
        var sut = new TestEntityWithDomainEvents();
        var domainEvent = new TestDomainEvent();

        // Act
        sut.RegisterEvent(domainEvent);

        // Assert
        Assert.Contains(domainEvent, sut.DomainEvents);
    }

    [Fact]
    public void ClearDomainEvents_ShouldClearEventList()
    {
        // Arrange
        var sut = new TestEntityWithDomainEvents();
        var domainEvent = new TestDomainEvent();
        sut.RegisterEvent(domainEvent);

        // Act
        sut.ClearDomainEvents();

        // Assert
        Assert.Empty(sut.DomainEvents);
    }
}
