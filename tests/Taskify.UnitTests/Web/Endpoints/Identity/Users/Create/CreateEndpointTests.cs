namespace Taskify.Web.Tests.Endpoints.Identity.Users.Create;

using System.Threading;
using System.Threading.Tasks;

using Ardalis.Result;

using FastEndpoints;
using MediatR;

using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

using Moq;

using Taskify.Identity.UseCases.Users.Create;
using Taskify.Web.Endpoints.Identity.Users.Create;

using Xunit;

public class CreateEndpointTests
{
    [Fact]
    public async Task HandleAsync_ShouldSendCreateUserCommand()
    {
        // Arrange
        var mediatorMock = new Mock<IMediator>();

        var linkgen = new Mock<LinkGenerator>();
        var endpoint = Factory.Create<CreateEndpoint>(ctx => ctx.AddTestServices(s => s.AddSingleton(linkgen.Object)), mediatorMock.Object);

        var request = new CreateUserDto();
        var cancellationToken = CancellationToken.None;

        var response = new Result<int>(1);
        mediatorMock.Setup(m => m.Send(It.IsAny<CreateUserCommand>(), cancellationToken)).ReturnsAsync(() => response);

        // Act
        await endpoint.HandleAsync(request, cancellationToken);

        // Assert
        mediatorMock.Verify(m => m.Send(It.IsAny<CreateUserCommand>(), cancellationToken), Times.Once);
    }
}
