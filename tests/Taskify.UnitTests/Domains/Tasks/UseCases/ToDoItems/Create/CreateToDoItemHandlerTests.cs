namespace Taskify.UnitTests.Domains.Tasks.UseCases.ToDoItems.Create;
using Taskify.Tasks.UseCases.ToDoItems.Create;

using System.Threading;
using System.Threading.Tasks;

using Ardalis.Result;

using Moq;

using Taskify.SharedKernel.Data;
using Taskify.Tasks.Core.ToDoItemAggregate;
using Taskify.Tasks.Core.ToDoItemAggregate.Events;

using Xunit;

public class CreateToDoItemHandlerTests
{
    [Fact]
    public async Task Handle_ValidRequest_ReturnsSuccessResultWithItemId()
    {
        // Arrange
        var repositoryMock = new Mock<IRepository<ToDoItem>>();
        var handler = new CreateToDoItemHandler(repositoryMock.Object);
        var command = new CreateToDoItemCommand(new CreateToDoItemDto());
        var cancellationToken = new CancellationToken();

        var createdItem = new ToDoItem { Id = 1 };

        repositoryMock.Setup(r => r.AddAsync(It.IsAny<ToDoItem>(), cancellationToken))
            .ReturnsAsync(createdItem);

        // Act
        var result = await handler.Handle(command, cancellationToken);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Status == ResultStatus.Ok);
        Assert.Equal(createdItem.Id, result.Value);
    }

    [Fact]
    public async Task Handle_MarksToDoItemAsNew()
    {
        // Arrange
        var repositoryMock = new Mock<IRepository<ToDoItem>>();
        var handler = new CreateToDoItemHandler(repositoryMock.Object);
        var command = new CreateToDoItemCommand(new CreateToDoItemDto());
        var cancellationToken = new CancellationToken();

        var createdItem = new ToDoItem();

        repositoryMock.Setup(r => r.AddAsync(
            It.Is<ToDoItem>(
                x => x.DomainEvents.Single().GetType() == typeof(ToDoItemCreatedEvent)),
            cancellationToken))
            .ReturnsAsync(createdItem);

        // Act
        await handler.Handle(command, cancellationToken);

        // Assert
        repositoryMock.Verify(r => r.AddAsync(
            It.Is<ToDoItem>(
                x => x.DomainEvents.Single().GetType() == typeof(ToDoItemCreatedEvent)),
            cancellationToken), Times.Exactly(1));
    }
}
