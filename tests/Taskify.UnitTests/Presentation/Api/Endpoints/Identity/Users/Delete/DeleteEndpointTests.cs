namespace Taskify.UnitTests.Presentation.Api.Endpoints.Identity.Users.Delete;

using System.Threading;
using System.Threading.Tasks;

using Ardalis.Result;

using FastEndpoints;
using MediatR;

using Moq;

using Taskify.Identity.UseCases.Users.Delete;
using Taskify.Api.Endpoints.Identity.Users.Delete;

using Xunit;

public class DeleteEndpointTests
{
    [Fact]
    public async Task HandleAsync_ShouldSendDeleteUserCommand()
    {
        // Arrange
        var mediatorMock = new Mock<IMediator>();
        var endpoint = Factory.Create<DeleteEndpoint>(mediatorMock.Object);
        var request = new DeleteUserCommand(1);
        var cancellationToken = CancellationToken.None;

        var response = new Result();
        mediatorMock.Setup(m => m.Send(It.IsAny<DeleteUserCommand>(), cancellationToken)).ReturnsAsync(() => response);

        // Act
        await endpoint.HandleAsync(request, cancellationToken);

        // Assert
        mediatorMock.Verify(m => m.Send(It.IsAny<DeleteUserCommand>(), cancellationToken), Times.Once);
    }
}
