namespace Taskify.Tasks.UseCases.Tests.ToDoItems.MarkToDoItemComplete;

using System.Threading;
using System.Threading.Tasks;

using Ardalis.Result;

using Moq;

using Taskify.SharedKernel.Data;
using Taskify.Tasks.Core.ToDoItemAggregate;
using Taskify.Tasks.Core.ToDoItemAggregate.Events;
using Taskify.Tasks.UseCases.ToDoItems.MarkToDoItemComplete;

using Xunit;

public class MarkToDoItemCompleteHandlerTests
{
    [Fact]
    public async Task Handle_ValidCommand_ItemMarkedComplete()
    {
        // Arrange
        var itemId = 1;
        var cancellationToken = CancellationToken.None;
        var repositoryMock = new Mock<IRepository<ToDoItem>>();
        var item = new ToDoItem() { Id = itemId };
        repositoryMock.Setup(r => r.GetByIdAsync(itemId, cancellationToken))
            .ReturnsAsync(item);
        var handler = new MarkToDoItemCompleteHandler(repositoryMock.Object);
        var command = new MarkToDoItemCompleteCommand(itemId);

        // Act
        var result = await handler.Handle(command, cancellationToken);

        // Assert
        Assert.True(result.Status == ResultStatus.Ok);
        Assert.True(item.IsComplete);
        repositoryMock.Verify(r => r.UpdateAsync(item, cancellationToken), Times.Once);
    }

    [Fact]
    public async Task Handle_InvalidCommand_ReturnsFailedResult()
    {
        // Arrange
        var itemId = 1;
        var cancellationToken = CancellationToken.None;
        var repositoryMock = new Mock<IRepository<ToDoItem>>();
        repositoryMock.Setup(r => r.GetByIdAsync(itemId, cancellationToken))
            .ReturnsAsync(() => null);
        var handler = new MarkToDoItemCompleteHandler(repositoryMock.Object);
        var command = new MarkToDoItemCompleteCommand(itemId);

        // Act
        var result =  await handler.Handle(command, cancellationToken);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ResultStatus.NotFound, result.Status);
        repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<ToDoItem>(), cancellationToken), Times.Never);
    }

    [Fact]
    public async Task Handle_CallsMarkItemComplete()
    {
        // Arrange
        var itemId = 1;
        var cancellationToken = CancellationToken.None;
        var repositoryMock = new Mock<IRepository<ToDoItem>>();
        var item = new ToDoItem() { Id = itemId };
        repositoryMock.Setup(r => r.GetByIdAsync(itemId, cancellationToken))
            .ReturnsAsync(item);
        var handler = new MarkToDoItemCompleteHandler(repositoryMock.Object);
        var command = new MarkToDoItemCompleteCommand(itemId);

        // Act
        await handler.Handle(command, cancellationToken);

        // Assert
        Assert.Single(item.DomainEvents);
        Assert.IsType<ToDoItemCompletedEvent>(item.DomainEvents.First());
    }
}
