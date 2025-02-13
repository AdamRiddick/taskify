namespace Taskify.UnitTests.Presentation.Api.Endpoints.Identity.ContextTypes.Create;

using System.Threading;
using System.Threading.Tasks;

using Ardalis.Result;

using FastEndpoints;
using MediatR;

using Moq;

using Taskify.Identity.UseCases.ContextTypes.Create;
using Taskify.Api.Endpoints.Identity.ContextTypes.Create;

using Xunit;

public class CreateEndpointTests
{
    [Fact]
    public async Task HandleAsync_ShouldSendCreateContextTypeCommand()
    {
        // Arrange
        var mediatorMock = new Mock<IMediator>();
        var endpoint = Factory.Create<CreateEndpoint>(mediatorMock.Object);
        var request = new CreateContextTypeDto();
        var cancellationToken = CancellationToken.None;

        var response = new Result();
        mediatorMock.Setup(m => m.Send(It.IsAny<CreateContextTypeCommand>(), cancellationToken)).ReturnsAsync(() => response);

        // Act
        await endpoint.HandleAsync(request, cancellationToken);

        // Assert
        mediatorMock.Verify(m => m.Send(It.IsAny<CreateContextTypeCommand>(), cancellationToken), Times.Once);
    }
}
