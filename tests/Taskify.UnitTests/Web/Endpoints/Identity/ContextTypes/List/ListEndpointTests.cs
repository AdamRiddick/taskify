namespace Taskify.Web.Tests.Endpoints.Identity.ContextTypes.Create;

using System.Threading;
using System.Threading.Tasks;

using Ardalis.Result;

using FastEndpoints;
using MediatR;

using Moq;

using Taskify.Identity.UseCases.ContextTypes.List;
using Taskify.Web.Endpoints.Identity.ContextTypes.List;

using Xunit;

public class ListEndpointTests
{
    [Fact]
    public async Task HandleAsync_ShouldSendListContextTypeCommand()
    {
        // Arrange
        var mediatorMock = new Mock<IMediator>();
        var endpoint = Factory.Create<ListEndpoint>(mediatorMock.Object);
        var request = new ListContextTypesQuery();
        var cancellationToken = CancellationToken.None;

        var response = new Result();
        mediatorMock.Setup(m => m.Send(It.IsAny<ListContextTypesQuery>(), cancellationToken)).ReturnsAsync(() => response);

        // Act
        await endpoint.HandleAsync(request, cancellationToken);

        // Assert
        mediatorMock.Verify(m => m.Send(It.IsAny<ListContextTypesQuery>(), cancellationToken), Times.Once);
    }
}
