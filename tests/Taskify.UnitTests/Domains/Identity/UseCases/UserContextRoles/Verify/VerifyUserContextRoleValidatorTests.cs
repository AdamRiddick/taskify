namespace Taskify.Identity.UseCases.UserContextRoles.Verify.Tests;

using Moq;

using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

using Taskify.Identity.Core.ContextTypeAggregate;
using Taskify.Identity.Core.UserAggregate;
using Taskify.SharedKernel.Data;

using Xunit;

public class VerifyUserContextRoleValidatorTests
{
    private readonly Mock<IReadRepository<ContextType>> _contextTypeRepositoryMock;
    private readonly Mock<IReadRepository<User>> _userRepositoryMock;

    public VerifyUserContextRoleValidatorTests()
    {
        _contextTypeRepositoryMock = new Mock<IReadRepository<ContextType>>();
        _userRepositoryMock = new Mock<IReadRepository<User>>();
    }

    [Fact]
    public async Task ValidateAsync_WhenContextTypeIsEmpty_ShouldReturnFalse()
    {
        // Arrange
        var validator = new VerifyUserContextRoleValidator(_contextTypeRepositoryMock.Object, _userRepositoryMock.Object);

        // Act
        var result = await validator.ValidateAsync(new VerifyUserContextRoleQuery(new VerifyUserContextRoleDto { ContextType = string.Empty }), CancellationToken.None);

        // Assert
        Assert.False(result.IsValid);
    }

    [Fact]
    public async Task ValidateAsync_WhenContextTypeDoesNotExist_ShouldReturnFalse()
    {
        // Arrange
        var contextType = new ContextType { Name = "TestContextType" };
        _contextTypeRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ContextType, bool>>>(), CancellationToken.None)).ReturnsAsync(() => null);
        var validator = new VerifyUserContextRoleValidator(_contextTypeRepositoryMock.Object, _userRepositoryMock.Object);

        // Act
        var result = await validator.ValidateAsync(new VerifyUserContextRoleQuery(new VerifyUserContextRoleDto { ContextType = contextType.Name }), CancellationToken.None);

        // Assert
        Assert.False(result.IsValid);
    }

    [Fact]
    public async Task ValidateAsync_WhenUserDoesNotExist_ShouldReturnFalse()
    {
        // Arrange
        var userId = 1;
        _userRepositoryMock.Setup(x => x.GetByIdAsync(1, CancellationToken.None)).ReturnsAsync(() => null);
        var validator = new VerifyUserContextRoleValidator(_contextTypeRepositoryMock.Object, _userRepositoryMock.Object);

        // Act
        var result = await validator.ValidateAsync(new VerifyUserContextRoleQuery(new VerifyUserContextRoleDto { UserId = userId }), CancellationToken.None);

        // Assert
        Assert.False(result.IsValid);
    }

    [Fact]
    public async Task ValidateAsync_WhenContextTypeAndUserExist_ShouldReturnTrue()
    {
        // Arrange
        var contextType = new ContextType { Name = "TestContextType" };
        var user = new User { Id = 1 };
        _contextTypeRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ContextType, bool>>>(), CancellationToken.None)).ReturnsAsync(contextType);
        _userRepositoryMock.Setup(x => x.GetByIdAsync(1, CancellationToken.None)).ReturnsAsync(user);
        var validator = new VerifyUserContextRoleValidator(_contextTypeRepositoryMock.Object, _userRepositoryMock.Object);

        // Act
        var result = await validator.ValidateAsync(new VerifyUserContextRoleQuery(new VerifyUserContextRoleDto { ContextType = contextType.Name, UserId = user.Id }), CancellationToken.None);

        // Assert
        Assert.True(result.IsValid);
    }
}
