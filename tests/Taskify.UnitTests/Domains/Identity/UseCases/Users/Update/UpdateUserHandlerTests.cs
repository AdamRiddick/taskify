namespace Taskify.Identity.UseCases.Users.Update.Tests;

using System.Threading;
using System.Threading.Tasks;

using Ardalis.Result;

using Moq;

using Taskify.Identity.Core.UserAggregate;
using Taskify.SharedKernel.Data;

using Xunit;

public class UpdateUserHandlerTests
{
    [Fact]
    public async Task Handle_ExistingItemExists_ShouldUpdateItemAndReturnSuccessResult()
    {
        // Arrange
        var existingItem = new User();
        var repositoryMock = new Mock<IRepository<User>>();
        repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingItem);
        var handler = new UpdateUserHandler(repositoryMock.Object);
        var command = new UpdateUserCommand(1, new UpdateUserDto());

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        repositoryMock.Verify(r => r.UpdateAsync(existingItem, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ExistingItemDoesNotExist_ShouldThrowException()
    {
        // Arrange
        var repositoryMock = new Mock<IRepository<User>>();
        repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => null);
        var handler = new UpdateUserHandler(repositoryMock.Object);
        var command = new UpdateUserCommand(1, new UpdateUserDto());

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ResultStatus.NotFound, result.Status);
        repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}
