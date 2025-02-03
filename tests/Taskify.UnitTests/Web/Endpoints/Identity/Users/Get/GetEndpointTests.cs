namespace Taskify.Web.Tests.Endpoints.Identity.Users.Create;

using System.Threading;
using System.Threading.Tasks;

using Ardalis.Result;

using FastEndpoints;
using MediatR;

using Moq;

using Taskify.Identity.UseCases.Users.Get;
using Taskify.Web.Endpoints.Identity.Users.Get;

using Xunit;

public class GetEndpointTests
{
    [Fact]
    public async Task HandleAsync_ShouldSendGetUserCommand()
    {
        // Arrange
        var mediatorMock = new Mock<IMediator>();
        var endpoint = Factory.Create<GetEndpoint>(mediatorMock.Object);
        var request = new GetUserQuery(1);
        var cancellationToken = CancellationToken.None;

        var response = new Result();
        mediatorMock.Setup(m => m.Send(It.IsAny<GetUserQuery>(), cancellationToken)).ReturnsAsync(() => response);

        // Act
        await endpoint.HandleAsync(request, cancellationToken);

        // Assert
        mediatorMock.Verify(m => m.Send(It.IsAny<GetUserQuery>(), cancellationToken), Times.Once);
    }
}
