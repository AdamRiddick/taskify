namespace Taskify.Tasks.UseCases.ToDoItems.Get.Tests;

using System.Threading;
using System.Threading.Tasks;

using Ardalis.Result;

using Moq;

using Taskify.SharedKernel.Data;
using Taskify.Tasks.Core.ToDoItemAggregate;

using Xunit;

public class GetToDoItemHandlerTests
{
    [Fact]
    public async Task Handle_ValidId_ReturnsResultWithDto()
    {
        // Arrange
        var itemId = 1;
        var item = new ToDoItem { Id = itemId, Title = "Test Item" };
        var repositoryMock = new Mock<IReadRepository<ToDoItem>>();
        repositoryMock.Setup(r => r.GetByIdAsync(itemId, CancellationToken.None)).ReturnsAsync(item);
        var handler = new GetToDoItemHandler(repositoryMock.Object);
        var query = new GetToDoItemQuery(itemId);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Status == ResultStatus.Ok);
        Assert.NotNull(result.Value);
        Assert.Equal(itemId, result.Value.Id);
        Assert.Equal(item.Title, result.Value.Title);
    }

    [Fact]
    public async Task Handle_InvalidId_ReturnsResultWithError()
    {
        // Arrange
        var itemId = 1;
        var repositoryMock = new Mock<IReadRepository<ToDoItem>>();
        repositoryMock.Setup(r => r.GetByIdAsync(itemId, CancellationToken.None)).ReturnsAsync(() => null);
        var handler = new GetToDoItemHandler(repositoryMock.Object);
        var query = new GetToDoItemQuery(itemId);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ResultStatus.NotFound, result.Status);
    }
}
