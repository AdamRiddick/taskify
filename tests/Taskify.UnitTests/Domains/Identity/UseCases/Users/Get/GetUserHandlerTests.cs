namespace Taskify.Identity.UseCases.Users.Get.Tests;

using System.Threading;
using System.Threading.Tasks;

using Ardalis.Result;

using Moq;

using Taskify.Identity.Core.UserAggregate;
using Taskify.SharedKernel.Data;

using Xunit;

public class GetUserHandlerTests
{
    [Fact]
    public async Task Handle_WithValidId_ReturnsUserDto()
    {
        // Arrange
        var userId = 1;
        var user = new User { Id = userId, Name = "John Doe" };
        var repositoryMock = new Mock<IReadRepository<User>>();
        repositoryMock.Setup(r => r.GetByIdAsync(userId, CancellationToken.None)).ReturnsAsync(user);
        var handler = new GetUserHandler(repositoryMock.Object);
        var query = new GetUserQuery(userId);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.IsType<Result<GetUserDto>>(result);
        Assert.Equal(userId, result.Value.Id);
        Assert.Equal(user.Name, result.Value.Name);
    }

    [Fact]
    public async Task Handle_WithInvalidId_ReturnsNull()
    {
        // Arrange
        var userId = 1;
        var repositoryMock = new Mock<IReadRepository<User>>();
        repositoryMock.Setup(r => r.GetByIdAsync(userId, CancellationToken.None)).ReturnsAsync(() => null);
        var handler = new GetUserHandler(repositoryMock.Object);
        var query = new GetUserQuery(userId);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.IsType<Result<GetUserDto>>(result);
        Assert.Null(result.Value);
        Assert.False(result.IsSuccess);
        Assert.Equal(ResultStatus.NotFound, result.Status);
    }
}
