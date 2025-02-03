namespace Taskify.Web.Tests.Endpoints.Tasks.ToDoItems.Create;

using System.Threading;
using System.Threading.Tasks;

using Ardalis.Result;

using FastEndpoints;
using MediatR;

using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

using Moq;

using Taskify.Tasks.UseCases.ToDoItems.Create;
using Taskify.Web.Endpoints.Tasks.ToDoItems.Create;

using Xunit;

public class CreateEndpointTests
{
    [Fact]
    public async Task HandleAsync_ShouldSendCreateToDoItemCommand()
    {
        // Arrange
        var mediatorMock = new Mock<IMediator>();

        var linkgen = new Mock<LinkGenerator>();
        var endpoint = Factory.Create<CreateEndpoint>(ctx => ctx.AddTestServices(s => s.AddSingleton(linkgen.Object)), mediatorMock.Object);

        var request = new CreateToDoItemDto();
        var cancellationToken = CancellationToken.None;

        var response = new Result<int>(1);
        mediatorMock.Setup(m => m.Send(It.IsAny<CreateToDoItemCommand>(), cancellationToken)).ReturnsAsync(() => response);

        // Act
        await endpoint.HandleAsync(request, cancellationToken);

        // Assert
        mediatorMock.Verify(m => m.Send(It.IsAny<CreateToDoItemCommand>(), cancellationToken), Times.Once);
    }
}
