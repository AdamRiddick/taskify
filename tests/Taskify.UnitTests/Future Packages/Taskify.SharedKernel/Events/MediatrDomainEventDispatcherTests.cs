namespace Taskify.SharedKernel.Events.Tests;

using System.Collections.Generic;
using System.Threading.Tasks;

using MediatR;

using Microsoft.Extensions.Logging;

using Moq;

using Taskify.UnitTests.TestHelpers.Objects;

using Xunit;

public class MediatrDomainEventDispatcherTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly Mock<ILogger<MediatrDomainEventDispatcher>> _loggerMock;
    private readonly MediatrDomainEventDispatcher _dispatcher;

    public MediatrDomainEventDispatcherTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _loggerMock = new Mock<ILogger<MediatrDomainEventDispatcher>>();
        _dispatcher = new MediatrDomainEventDispatcher(_mediatorMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task DispatchAndClearEvents_WithEntitiesWithEvents_CallsMediatorPublishForEachEvent()
    {
        // Arrange
        var entity1 = new TestEntityWithDomainEvents();
        var entity2 = new TestEntityWithDomainEvents();
        var entitiesWithEvents = new List<IHasDomainEvents> { entity1, entity2 };

        var domainEvent1 = new TestDomainEvent();
        var domainEvent2 = new TestDomainEvent();

        entity1.RegisterEvent(domainEvent1);
        entity2.RegisterEvent(domainEvent2);

        // Act
        await _dispatcher.DispatchAndClearEvents(entitiesWithEvents);

        // Assert
        _mediatorMock.Verify(m => m.Publish<DomainEventBase>(It.Is<TestDomainEvent>(x => x.Guid.Equals(domainEvent1.Guid)), It.IsAny<CancellationToken>()), Times.Once);
        _mediatorMock.Verify(m => m.Publish<DomainEventBase>(It.Is<TestDomainEvent>(x => x.Guid.Equals(domainEvent2.Guid)), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task DispatchAndClearEvents_WithEntitiesWithoutEvents_LogsError()
    {
        // Arrange
        var entity = new Mock<IHasDomainEvents>();
        var entitiesWithEvents = new List<IHasDomainEvents> { entity.Object };

        // Act
        await _dispatcher.DispatchAndClearEvents(entitiesWithEvents);

        // Assert
        _loggerMock.Verify(
            l => l.Log(
                It.Is<LogLevel>(level => level == LogLevel.Error),
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }
}
