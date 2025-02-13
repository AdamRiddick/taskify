namespace Taskify.UnitTests.Presentation.Api.Endpoints.Tasks.ToDoItems.MarkToDoItemComplete;

using System.Threading;
using System.Threading.Tasks;

using Ardalis.Result;

using FastEndpoints;
using MediatR;

using Moq;

using Taskify.Tasks.UseCases.ToDoItems.MarkToDoItemComplete;
using Taskify.Api.Endpoints.Tasks.ToDoItems.MarkToDoItemComplete;

using Xunit;

public class MarkEndpointTests
{
    [Fact]
    public async Task HandleAsync_ShouldSendGetToDoItemCommand()
    {
        // Arrange
        var mediatorMock = new Mock<IMediator>();
        var endpoint = Factory.Create<MarkCompleteEndpoint>(mediatorMock.Object);
        var request = new MarkToDoItemCompleteCommand(1);
        var cancellationToken = CancellationToken.None;

        var response = new Result();
        mediatorMock.Setup(m => m.Send(It.IsAny<MarkToDoItemCompleteCommand>(), cancellationToken)).ReturnsAsync(() => response);

        // Act
        await endpoint.HandleAsync(request, cancellationToken);

        // Assert
        mediatorMock.Verify(m => m.Send(It.IsAny<MarkToDoItemCompleteCommand>(), cancellationToken), Times.Once);
    }
}
