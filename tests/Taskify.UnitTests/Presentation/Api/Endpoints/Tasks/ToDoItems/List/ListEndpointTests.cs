namespace Taskify.UnitTests.Presentation.Api.Endpoints.Tasks.ToDoItems.List;

using System.Threading;
using System.Threading.Tasks;

using Ardalis.Result;

using FastEndpoints;
using MediatR;

using Moq;

using Taskify.Tasks.UseCases.ToDoItems.List;
using Taskify.Api.Endpoints.Tasks.ToDoItems.List;

using Xunit;

public class ListEndpointTests
{
    [Fact]
    public async Task HandleAsync_ShouldSendListToDoItemCommand()
    {
        // Arrange
        var mediatorMock = new Mock<IMediator>();
        var endpoint = Factory.Create<ListEndpoint>(mediatorMock.Object);
        var request = new ListToDoItemsQuery();
        var cancellationToken = CancellationToken.None;

        var response = new Result();
        mediatorMock.Setup(m => m.Send(It.IsAny<ListToDoItemsQuery>(), cancellationToken)).ReturnsAsync(() => response);

        // Act
        await endpoint.HandleAsync(request, cancellationToken);

        // Assert
        mediatorMock.Verify(m => m.Send(It.IsAny<ListToDoItemsQuery>(), cancellationToken), Times.Once);
    }
}
