namespace Taskify.UnitTests.Future_Packages.Taskify.Infrastructure.Ef;

using global::Taskify.SharedKernel.Events;
using global::Taskify.UnitTests.TestHelpers;
using global::Taskify.UnitTests.TestHelpers.Objects;

using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

public class AppDbContextBaseTests
{
    [Fact]
    public async Task SaveChangesAsync_WithDispatcher_DispatchesEvents()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<TestDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

        var dispatcherMock = new Mock<IDomainEventDispatcher>();
        var entityWithEvents = new TestEntityWithDomainEvents();
        entityWithEvents.RegisterEvent(new TestDomainEvent());

        using (var context = new TestDbContext(options, dispatcherMock.Object))
        {
            context.Set<TestEntityWithDomainEvents>().Add(entityWithEvents);
            await context.SaveChangesAsync();
        }

        // Assert
        dispatcherMock.Verify(d => d.DispatchAndClearEvents(It.IsAny<HasDomainEventsBase[]>()), Times.Once);
    }

    [Fact]
    public void SaveChanges_WithDispatcher_DispatchesEvents()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<TestDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

        var dispatcherMock = new Mock<IDomainEventDispatcher>();
        var entityWithEvents = new TestEntityWithDomainEvents();
        entityWithEvents.RegisterEvent(new TestDomainEvent());

        using (var context = new TestDbContext(options, dispatcherMock.Object))
        {
            context.Set<TestEntityWithDomainEvents>().Add(entityWithEvents);
            context.SaveChanges();
        }

        // Assert
        dispatcherMock.Verify(d => d.DispatchAndClearEvents(It.IsAny<HasDomainEventsBase[]>()), Times.Once);
    }
}
