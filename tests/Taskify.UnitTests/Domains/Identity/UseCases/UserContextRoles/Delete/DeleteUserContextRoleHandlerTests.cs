namespace Taskify.Identity.UseCases.UserContextRoles.Delete.Tests;

using System.Threading;
using System.Threading.Tasks;

using Ardalis.Result;

using Moq;

using Taskify.Identity.Core.UserContextRoleAggregate;
using Taskify.SharedKernel.Data;

using Xunit;


public class DeleteUserContextRoleHandlerTests
{
    [Fact]
    public async Task Handle_WithValidRequest_ShouldDeleteUserContextRole()
    {
        // Arrange
        var repositoryMock = new Mock<IRepository<UserContextRole>>();
        var handler = new DeleteUserContextRoleHandler(repositoryMock.Object);
        var request = new DeleteUserContextRoleCommand(1);
        var cancellationToken = CancellationToken.None;

        var userContextRole = new UserContextRole { Id = 1 };

        repositoryMock.Setup(r => r.GetByIdAsync(request.Id, cancellationToken))
            .ReturnsAsync(userContextRole);

        // Act
        var result = await handler.Handle(request, cancellationToken);

        // Assert
        Assert.True(result.IsSuccess);
        repositoryMock.Verify(r => r.DeleteAsync(userContextRole, cancellationToken), Times.Once);
    }

    [Fact]
    public async Task Handle_WithInvalidRequest_ShouldReturnFailureResult()
    {
        // Arrange
        var repositoryMock = new Mock<IRepository<UserContextRole>>();
        var handler = new DeleteUserContextRoleHandler(repositoryMock.Object);
        var request = new DeleteUserContextRoleCommand(1);
        var cancellationToken = CancellationToken.None;

        repositoryMock.Setup(r => r.GetByIdAsync(request.Id, cancellationToken))
            .ReturnsAsync(() => null);

        // Act
        var result = await handler.Handle(request, cancellationToken);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ResultStatus.NotFound, result.Status);
        repositoryMock.Verify(r => r.DeleteAsync(It.IsAny<UserContextRole>(), cancellationToken), Times.Never);
    }
}
