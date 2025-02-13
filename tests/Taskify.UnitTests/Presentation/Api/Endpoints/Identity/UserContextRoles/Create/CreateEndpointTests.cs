namespace Taskify.UnitTests.Presentation.Api.Endpoints.Identity.UserContextRoles.Create;

using System.Threading;
using System.Threading.Tasks;

using Ardalis.Result;

using FastEndpoints;
using MediatR;

using Moq;

using Taskify.Identity.UseCases.UserContextRoles.Create;
using Taskify.Api.Endpoints.Identity.UserContextRoles.Create;

using Xunit;

public class CreateEndpointTests
{
    [Fact]
    public async Task HandleAsync_ShouldSendCreateUserContextRoleCommand()
    {
        // Arrange
        var mediatorMock = new Mock<IMediator>();
        var endpoint = Factory.Create<CreateEndpoint>(mediatorMock.Object);
        var request = new CreateUserContextRoleDto();
        var cancellationToken = CancellationToken.None;

        var response = new Result();
        mediatorMock.Setup(m => m.Send(It.IsAny<CreateUserContextRoleCommand>(), cancellationToken)).ReturnsAsync(() => response);

        // Act
        await endpoint.HandleAsync(request, cancellationToken);

        // Assert
        mediatorMock.Verify(m => m.Send(It.IsAny<CreateUserContextRoleCommand>(), cancellationToken), Times.Once);
    }
}
