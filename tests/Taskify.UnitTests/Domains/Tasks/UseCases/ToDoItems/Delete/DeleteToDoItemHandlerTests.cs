namespace Taskify.UnitTests.Domains.Tasks.UseCases.ToDoItems.Delete;
using Taskify.Tasks.UseCases.ToDoItems.Delete;

using System.Threading;
using System.Threading.Tasks;

using Ardalis.Result;

using Moq;

using Taskify.SharedKernel.Data;
using Taskify.Tasks.Core.ToDoItemAggregate;

using Xunit;

public class DeleteToDoItemHandlerTests
{
    [Fact]
    public async Task Handle_ValidRequest_DeletesToDoItem()
    {
        // Arrange
        var itemId = 1;
        var cancellationToken = CancellationToken.None;
        var repositoryMock = new Mock<IRepository<ToDoItem>>();
        var handler = new DeleteToDoItemHandler(repositoryMock.Object);
        var itemToDelete = new ToDoItem() { Id = itemId };

        repositoryMock.Setup(r => r.GetByIdAsync(itemId, cancellationToken))
            .ReturnsAsync(itemToDelete);

        // Act
        var result = await handler.Handle(new DeleteToDoItemCommand(itemId), cancellationToken);

        // Assert
        repositoryMock.Verify(r => r.DeleteAsync(itemToDelete, cancellationToken), Times.Once);
        Assert.Equal(ResultStatus.Ok, result.Status);
    }

    [Fact]
    public async Task Handle_InvalidRequest_ReturnsFailedResult()
    {
        // Arrange
        var itemId = 1;
        var cancellationToken = CancellationToken.None;
        var repositoryMock = new Mock<IRepository<ToDoItem>>();
        var handler = new DeleteToDoItemHandler(repositoryMock.Object);

        repositoryMock.Setup(r => r.GetByIdAsync(itemId, cancellationToken))
            .ReturnsAsync(() => null);

        // Act
        var result = await handler.Handle(new DeleteToDoItemCommand(itemId), cancellationToken);

        // Assert
        repositoryMock.Verify(r => r.DeleteAsync(It.IsAny<ToDoItem>(), cancellationToken), Times.Never);
        Assert.NotNull(result);
        Assert.Equal(ResultStatus.NotFound, result.Status);
    }
}
