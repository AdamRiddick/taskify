namespace Taskify.UnitTests.Presentation.Api.Endpoints.Tasks.ToDoItems.Update;

using System.Threading;
using System.Threading.Tasks;

using Ardalis.Result;

using FastEndpoints;

using MediatR;

using Moq;

using Taskify.Tasks.UseCases.ToDoItems.Update;
using Taskify.Api.Endpoints.Tasks.ToDoItems.Update;

using Xunit;

public class UpdateEndpointTests
{
    [Fact]
    public async Task HandleAsync_ShouldSendUpdateToDoItemCommand()
    {
        // Arrange
        var mediatorMock = new Mock<IMediator>();

        var endpoint = Factory.Create<UpdateEndpoint>(ctx => ctx.Request.RouteValues.Add("id", "1"), mediatorMock.Object);
        var request = new UpdateToDoItemDto();
        var cancellationToken = CancellationToken.None;

        var response = new Result();
        mediatorMock.Setup(m => m.Send(It.IsAny<UpdateToDoItemCommand>(), cancellationToken)).ReturnsAsync(() => response);

        // Act
        await endpoint.HandleAsync(request, cancellationToken);

        // Assert
        mediatorMock.Verify(m => m.Send(It.IsAny<UpdateToDoItemCommand>(), cancellationToken), Times.Once);
    }
}
