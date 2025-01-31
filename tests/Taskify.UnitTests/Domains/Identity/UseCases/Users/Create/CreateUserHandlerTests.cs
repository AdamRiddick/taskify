namespace Taskify.Identity.UseCases.Users.Create.Tests;

using System.Threading;
using System.Threading.Tasks;

using Ardalis.Result;

using Moq;

using Taskify.Identity.Core.UserAggregate;
using Taskify.SharedKernel.Data;

using Xunit;

public class CreateUserHandlerTests
{
    [Fact]
    public async Task Handle_ValidRequest_ReturnsCreatedResultWithId()
    {
        // Arrange
        var repositoryMock = new Mock<IRepository<User>>();
        var handler = new CreateUserHandler(repositoryMock.Object);
        var command = new CreateUserCommand(new CreateUserDto());
        var cancellationToken = new CancellationToken();

        var createdItem = new User { Id = 1 };

        repositoryMock.Setup(r => r.AddAsync(It.IsAny<User>(), cancellationToken))
            .ReturnsAsync(createdItem);

        // Act
        var result = await handler.Handle(command, cancellationToken);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Status == ResultStatus.Created);
        Assert.Equal(createdItem.Id, result.Value);
    }
}
