namespace Taskify.Web.Tests.Endpoints.Identity.Users.Create;

using System.Threading;
using System.Threading.Tasks;

using Ardalis.Result;

using FastEndpoints;
using MediatR;

using Moq;

using Taskify.Identity.UseCases.Users.List;
using Taskify.Web.Endpoints.Identity.Users.List;

using Xunit;

public class ListEndpointTests
{
    [Fact]
    public async Task HandleAsync_ShouldSendListUserCommand()
    {
        // Arrange
        var mediatorMock = new Mock<IMediator>();
        var endpoint = Factory.Create<ListEndpoint>(mediatorMock.Object);
        var request = new ListUsersQuery();
        var cancellationToken = CancellationToken.None;

        var response = new Result();
        mediatorMock.Setup(m => m.Send(It.IsAny<ListUsersQuery>(), cancellationToken)).ReturnsAsync(() => response);

        // Act
        await endpoint.HandleAsync(request, cancellationToken);

        // Assert
        mediatorMock.Verify(m => m.Send(It.IsAny<ListUsersQuery>(), cancellationToken), Times.Once);
    }
}
