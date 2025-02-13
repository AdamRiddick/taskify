namespace Taskify.UnitTests.Presentation.Api.Endpoints.Identity.Users.Get;

using System.Threading;
using System.Threading.Tasks;

using Ardalis.Result;

using FastEndpoints;
using MediatR;

using Moq;

using Taskify.Identity.UseCases.Users.Get;
using Taskify.Api.Endpoints.Identity.Users.Get;

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
