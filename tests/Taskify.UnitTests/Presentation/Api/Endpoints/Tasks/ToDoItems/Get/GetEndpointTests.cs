namespace Taskify.UnitTests.Presentation.Api.Endpoints.Tasks.ToDoItems.Get;

using System.Threading;
using System.Threading.Tasks;

using Ardalis.Result;

using FastEndpoints;
using MediatR;

using Moq;

using Taskify.Tasks.UseCases.ToDoItems.Get;
using Taskify.Api.Endpoints.Tasks.ToDoItems.Get;

using Xunit;

public class GetEndpointTests
{
    [Fact]
    public async Task HandleAsync_ShouldSendGetToDoItemCommand()
    {
        // Arrange
        var mediatorMock = new Mock<IMediator>();
        var endpoint = Factory.Create<GetEndpoint>(mediatorMock.Object);
        var request = new GetToDoItemQuery(1);
        var cancellationToken = CancellationToken.None;

        var response = new Result();
        mediatorMock.Setup(m => m.Send(It.IsAny<GetToDoItemQuery>(), cancellationToken)).ReturnsAsync(() => response);

        // Act
        await endpoint.HandleAsync(request, cancellationToken);

        // Assert
        mediatorMock.Verify(m => m.Send(It.IsAny<GetToDoItemQuery>(), cancellationToken), Times.Once);
    }
}
