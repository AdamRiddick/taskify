namespace Taskify.UnitTests.Domains.Tasks.UseCases.ToDoItems.List;
using Taskify.Tasks.UseCases.ToDoItems.List;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Ardalis.Result;

using Moq;

using Taskify.SharedKernel.Data;
using Taskify.Tasks.Core.ToDoItemAggregate;

using Xunit;

public class ListToDoItemsHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnListOfToDoItemDtos()
    {
        // Arrange
        var repositoryMock = new Mock<IReadRepository<ToDoItem>>();
        var handler = new ListToDoItemsHandler(repositoryMock.Object);
        var query = new ListToDoItemsQuery();
        var cancellationToken = new CancellationToken();

        var items = new List<ToDoItem>
        {
            new ToDoItem { Id = 1, Title = "Task 1" },
            new ToDoItem { Id = 2, Title = "Task 2" },
            new ToDoItem { Id = 3, Title = "Task 3" }
        };

        repositoryMock.Setup(r => r.GetAllAsync(CancellationToken.None)).ReturnsAsync(items);

        // Act
        var result = await handler.Handle(query, cancellationToken);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<Result<IEnumerable<ListToDoItemDto>>>(result);
        Assert.Equal(items.Count, result.Value.Count());
    }
}
