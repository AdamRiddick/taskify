namespace Taskify.Tasks.UseCases.ToDoItems.Update.Tests;

using System.Threading;
using System.Threading.Tasks;

using Ardalis.Result;

using Moq;

using Taskify.SharedKernel.Data;
using Taskify.Tasks.Core.ToDoItemAggregate;

using Xunit;

public class UpdateToDoItemHandlerTests
{
    [Fact]
    public async Task Handle_ExistingItemExists_UpdatesItemAndReturnsSuccessResult()
    {
        // Arrange
        var itemId = 1;
        var cancellationToken = CancellationToken.None;
        var existingItem = new ToDoItem { Id = itemId };
        var repositoryMock = new Mock<IRepository<ToDoItem>>();
        repositoryMock.Setup(r => r.GetByIdAsync(itemId, cancellationToken))
            .ReturnsAsync(existingItem);
        var handler = new UpdateToDoItemHandler(repositoryMock.Object);
        var command = new UpdateToDoItemCommand(itemId, new UpdateToDoItemDto());

        // Act
        var result = await handler.Handle(command, cancellationToken);

        // Assert
        Assert.True(result.IsSuccess);
        repositoryMock.Verify(r => r.GetByIdAsync(itemId, cancellationToken), Times.Once);
        repositoryMock.Verify(r => r.UpdateAsync(existingItem, cancellationToken), Times.Once);
    }

    [Fact]
    public async Task Handle_ExistingItemDoesNotExist_ReturnsFailureResult()
    {
        // Arrange
        var itemId = 1;
        var cancellationToken = CancellationToken.None;
        var repositoryMock = new Mock<IRepository<ToDoItem>>();
        repositoryMock.Setup(r => r.GetByIdAsync(itemId, cancellationToken))
            .ReturnsAsync(() => null);
        var handler = new UpdateToDoItemHandler(repositoryMock.Object);
        var command = new UpdateToDoItemCommand(itemId, new UpdateToDoItemDto());

        // Act
        var result = await handler.Handle(command, cancellationToken);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ResultStatus.NotFound, result.Status);
        repositoryMock.Verify(r => r.GetByIdAsync(itemId, cancellationToken), Times.Once);
        repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<ToDoItem>(), cancellationToken), Times.Never);
    }
}
