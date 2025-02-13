namespace Taskify.UnitTests.Domains.Identity.UseCases.UserContextRoles.Create;
using Taskify.Identity.UseCases.UserContextRoles.Create;

using System.Threading;
using System.Threading.Tasks;

using Ardalis.Result;

using Moq;

using Taskify.Identity.Core.UserContextRoleAggregate;
using Taskify.SharedKernel.Data;

using Xunit;

public class CreateUserContextRoleHandlerTests
{
    [Fact]
    public async Task Handle_ValidRequest_ReturnsCreatedResultWithId()
    {
        // Arrange
        var dto = new CreateUserContextRoleDto();
        var request = new CreateUserContextRoleCommand(dto);
        var cancellationToken = new CancellationToken();
        var createdItem = new UserContextRole { Id = 1 };

        var repositoryMock = new Mock<IRepository<UserContextRole>>();
        repositoryMock.Setup(r => r.AddAsync(It.IsAny<UserContextRole>(), cancellationToken))
            .ReturnsAsync(createdItem);

        var handler = new CreateUserContextRoleHandler(repositoryMock.Object);

        // Act
        var result = await handler.Handle(request, cancellationToken);

        // Assert
        Assert.IsType<Result<int>>(result);
        Assert.Equal(ResultStatus.Created, result.Status);
        Assert.Equal(createdItem.Id, result.Value);
    }
}
