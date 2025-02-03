namespace Taskify.Web.Endpoints.Identity.UserContextRoles.Create;

using Moq;

using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

using Taskify.Identity.Core.ContextTypeAggregate;
using Taskify.Identity.Core.UserAggregate;
using Taskify.Identity.Core.UserContextRoleAggregate;
using Taskify.Identity.UseCases.UserContextRoles.Create;
using Taskify.SharedKernel.Data;
using Taskify.SharedKernel.Security;

using Xunit;

public class CreateValidatorTests
{
    private readonly Mock<IReadRepository<ContextType>> _contextTypeRepositoryMock;
    private readonly Mock<IReadRepository<User>> _userRepositoryMock;
    private readonly Mock<IReadRepository<UserContextRole>> _userContextRoleRepositoryMock;

    private readonly CreateValidator _validator;

    public CreateValidatorTests()
    {
        _contextTypeRepositoryMock = new Mock<IReadRepository<ContextType>>();
        _userRepositoryMock = new Mock<IReadRepository<User>>();
        _userContextRoleRepositoryMock = new Mock<IReadRepository<UserContextRole>>();

        _validator = new CreateValidator(
            _contextTypeRepositoryMock.Object,
            _userRepositoryMock.Object,
            _userContextRoleRepositoryMock.Object);
    }

    [Fact]
    public async Task ValidateAsync_WhenUserDoesNotExist_ShouldHaveValidationError()
    {
        // Arrange
        var command = new CreateUserContextRoleCommand(new CreateUserContextRoleDto { UserId = 1 });
        _userRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => null);

        // Act
        var result = await _validator.ValidateAsync(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.ErrorMessage == "User does not exist.");
    }

    [Fact]
    public async Task ValidateAsync_WhenContextTypeDoesNotExist_ShouldHaveValidationError()
    {
        // Arrange
        var command = new CreateUserContextRoleCommand(new CreateUserContextRoleDto { ContextTypeId = 1 });
        _contextTypeRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => null);

        // Act
        var result = await _validator.ValidateAsync(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.ErrorMessage == "Context Type does not exist.");
    }

    [Fact]
    public async Task ValidateAsync_WhenUserContextRoleAlreadyExists_ShouldHaveValidationError()
    {
        // Arrange
        var command = new CreateUserContextRoleCommand(new CreateUserContextRoleDto
        {
            ContextId = 1,
            Role = Role.Owner,
            UserId = 1
        });

        _userContextRoleRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Expression<Func<UserContextRole, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new UserContextRole());

        // Act
        var result = await _validator.ValidateAsync(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.ErrorMessage == "User Context Role Already Exists.");
    }

    [Fact]
    public async Task ValidateAsync_WhenValidationPasses_ShouldBeValid()
    {
        // Arrange
        var command = new CreateUserContextRoleCommand(new CreateUserContextRoleDto
        {
            ContextId = 1,
            Role = Role.Contributor,
            UserId = 1
        });
        _userRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new User());
        _contextTypeRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ContextType());
        _userContextRoleRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Expression<Func<UserContextRole, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => null);

        // Act
        var result = await _validator.ValidateAsync(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }
}
