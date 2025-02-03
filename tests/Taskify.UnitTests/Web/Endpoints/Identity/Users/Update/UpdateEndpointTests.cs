namespace Taskify.Web.Tests.Endpoints.Identity.Users.Update;

using System.Threading;
using System.Threading.Tasks;

using Ardalis.Result;

using FastEndpoints;

using MediatR;

using Moq;

using Taskify.Identity.UseCases.Users.Update;
using Taskify.Web.Endpoints.Identity.Users.Update;

using Xunit;

public class UpdateEndpointTests
{
    [Fact]
    public async Task HandleAsync_ShouldSendUpdateUserCommand()
    {
        // Arrange
        var mediatorMock = new Mock<IMediator>();

        var endpoint = Factory.Create<UpdateEndpoint>(ctx => ctx.Request.RouteValues.Add("id", "1"), mediatorMock.Object);
        var request = new UpdateUserDto();
        var cancellationToken = CancellationToken.None;

        var response = new Result();
        mediatorMock.Setup(m => m.Send(It.IsAny<UpdateUserCommand>(), cancellationToken)).ReturnsAsync(() => response);

        // Act
        await endpoint.HandleAsync(request, cancellationToken);

        // Assert
        mediatorMock.Verify(m => m.Send(It.IsAny<UpdateUserCommand>(), cancellationToken), Times.Once);
    }
}
