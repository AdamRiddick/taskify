namespace Taskify.Web.Tests.Endpoints.Identity.UserContextRoles.Create;

using System.Threading;
using System.Threading.Tasks;

using Ardalis.Result;

using FastEndpoints;
using MediatR;

using Moq;

using Taskify.Identity.UseCases.UserContextRoles.Delete;
using Taskify.Web.Endpoints.Identity.UserContextRoles.Delete;

using Xunit;

public class DeleteEndpointTests
{
    [Fact]
    public async Task HandleAsync_ShouldSendDeleteUserContextRoleCommand()
    {
        // Arrange
        var mediatorMock = new Mock<IMediator>();
        var endpoint = Factory.Create<DeleteEndpoint>(mediatorMock.Object);
        var request = new DeleteUserContextRoleCommand(1);
        var cancellationToken = CancellationToken.None;

        var response = new Result();
        mediatorMock.Setup(m => m.Send(It.IsAny<DeleteUserContextRoleCommand>(), cancellationToken)).ReturnsAsync(() => response);

        // Act
        await endpoint.HandleAsync(request, cancellationToken);

        // Assert
        mediatorMock.Verify(m => m.Send(It.IsAny<DeleteUserContextRoleCommand>(), cancellationToken), Times.Once);
    }
}
