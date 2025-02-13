namespace Taskify.UnitTests.Presentation.Api.Endpoints.Identity.UserContextRoles.List;

using System.Threading;
using System.Threading.Tasks;

using Ardalis.Result;

using FastEndpoints;
using MediatR;

using Moq;

using Taskify.Identity.UseCases.UserContextRoles.List;
using Taskify.Api.Endpoints.Identity.UserContextRoles.List;

using Xunit;

public class ListEndpointTests
{
    [Fact]
    public async Task HandleAsync_ShouldSendListUserContextRoleCommand()
    {
        // Arrange
        var mediatorMock = new Mock<IMediator>();
        var endpoint = Factory.Create<ListEndpoint>(mediatorMock.Object);
        var request = new ListUserContextRolesQuery();
        var cancellationToken = CancellationToken.None;

        var response = new Result();
        mediatorMock.Setup(m => m.Send(It.IsAny<ListUserContextRolesQuery>(), cancellationToken)).ReturnsAsync(() => response);

        // Act
        await endpoint.HandleAsync(request, cancellationToken);

        // Assert
        mediatorMock.Verify(m => m.Send(It.IsAny<ListUserContextRolesQuery>(), cancellationToken), Times.Once);
    }
}
