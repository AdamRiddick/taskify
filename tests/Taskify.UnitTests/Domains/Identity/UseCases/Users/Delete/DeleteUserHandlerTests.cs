namespace Taskify.Identity.UseCases.Users.Delete;

using System.Threading;
using System.Threading.Tasks;

using Ardalis.Result;

using Moq;

using Taskify.Identity.Core.UserAggregate;
using Taskify.SharedKernel.Data;

using Xunit;

public class DeleteUserHandlerTests
{
    [Fact]
    public async Task Handle_WithValidRequest_ShouldDeleteUser()
    {
        // Arrange
        var userId = 1;
        var cancellationToken = CancellationToken.None;
        var user = new User { Id = userId };
        var repositoryMock = new Mock<IRepository<User>>();
        repositoryMock.Setup(r => r.GetByIdAsync(userId, cancellationToken))
            .ReturnsAsync(user);
        var handler = new DeleteUserHandler(repositoryMock.Object);

        // Act
        var result = await handler.Handle(new DeleteUserCommand(userId), cancellationToken);

        // Assert
        Assert.True(result.IsSuccess);
        repositoryMock.Verify(r => r.DeleteAsync(user, cancellationToken), Times.Once);
    }

    [Fact]
    public async Task Handle_WithInvalidRequest_ShouldReturnFailureResult()
    {
        // Arrange
        var userId = 1;
        var cancellationToken = CancellationToken.None;
        var repositoryMock = new Mock<IRepository<User>>();
        repositoryMock.Setup(r => r.GetByIdAsync(userId, cancellationToken))
            .ReturnsAsync(() => null);
        var handler = new DeleteUserHandler(repositoryMock.Object);

        // Act
        var result = await handler.Handle(new DeleteUserCommand(userId), cancellationToken);

        // Assert
        Assert.False(result.IsSuccess);
        repositoryMock.Verify(r => r.DeleteAsync(It.IsAny<User>(), cancellationToken), Times.Never);
    }
}
