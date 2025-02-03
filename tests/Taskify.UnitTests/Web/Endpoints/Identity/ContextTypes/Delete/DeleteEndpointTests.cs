namespace Taskify.Web.Tests.Endpoints.Identity.ContextTypes.Create;

using System.Threading;
using System.Threading.Tasks;

using Ardalis.Result;

using FastEndpoints;
using MediatR;

using Moq;

using Taskify.Identity.UseCases.ContextTypes.Delete;
using Taskify.Web.Endpoints.Identity.ContextTypes.Delete;

using Xunit;

public class DeleteEndpointTests
{
    [Fact]
    public async Task HandleAsync_ShouldSendDeleteContextTypeCommand()
    {
        // Arrange
        var mediatorMock = new Mock<IMediator>();
        var endpoint = Factory.Create<DeleteEndpoint>(mediatorMock.Object);
        var request = new DeleteContextTypeCommand(1);
        var cancellationToken = CancellationToken.None;

        var response = new Result();
        mediatorMock.Setup(m => m.Send(It.IsAny<DeleteContextTypeCommand>(), cancellationToken)).ReturnsAsync(() => response);

        // Act
        await endpoint.HandleAsync(request, cancellationToken);

        // Assert
        mediatorMock.Verify(m => m.Send(It.IsAny<DeleteContextTypeCommand>(), cancellationToken), Times.Once);
    }
}
