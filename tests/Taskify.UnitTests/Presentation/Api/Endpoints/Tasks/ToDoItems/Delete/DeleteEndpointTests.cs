namespace Taskify.UnitTests.Presentation.Api.Endpoints.Tasks.ToDoItems.Delete;

using System.Threading;
using System.Threading.Tasks;

using Ardalis.Result;

using FastEndpoints;
using MediatR;

using Moq;

using Taskify.Tasks.UseCases.ToDoItems.Delete;
using Taskify.Api.Endpoints.Tasks.ToDoItems.Delete;

using Xunit;

public class DeleteEndpointTests
{
    [Fact]
    public async Task HandleAsync_ShouldSendDeleteToDoItemCommand()
    {
        // Arrange
        var mediatorMock = new Mock<IMediator>();
        var endpoint = Factory.Create<DeleteEndpoint>(mediatorMock.Object);
        var request = new DeleteToDoItemCommand(1);
        var cancellationToken = CancellationToken.None;

        var response = new Result();
        mediatorMock.Setup(m => m.Send(It.IsAny<DeleteToDoItemCommand>(), cancellationToken)).ReturnsAsync(() => response);

        // Act
        await endpoint.HandleAsync(request, cancellationToken);

        // Assert
        mediatorMock.Verify(m => m.Send(It.IsAny<DeleteToDoItemCommand>(), cancellationToken), Times.Once);
    }
}
