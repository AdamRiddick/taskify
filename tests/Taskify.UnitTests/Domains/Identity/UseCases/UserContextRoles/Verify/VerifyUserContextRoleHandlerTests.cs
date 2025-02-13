namespace Taskify.UnitTests.Domains.Identity.UseCases.UserContextRoles.Verify;
using Taskify.Identity.UseCases.UserContextRoles.Verify;

using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

using Ardalis.Result;

using Moq;

using Taskify.Identity.Core.ContextTypeAggregate;
using Taskify.Identity.Core.UserContextRoleAggregate;
using Taskify.SharedKernel.Data;
using Taskify.SharedKernel.Security;

using Xunit;

public class VerifyUserContextRoleHandlerTests
{
    private readonly Mock<IRepository<UserContextRole>> _repositoryMock;
    private readonly VerifyUserContextRoleHandler _handler;

    public VerifyUserContextRoleHandlerTests()
    {
        _repositoryMock = new Mock<IRepository<UserContextRole>>();
        _handler = new VerifyUserContextRoleHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task Handle_WithValidRequest_ReturnsAccessAllowed()
    {
        // Arrange
        var dto = new VerifyUserContextRoleDto
        {
            UserId = 1,
            ContextId = 1,
            ContextType = "SomeContextType",
            Role = Role.Owner
        };
        var query = new VerifyUserContextRoleQuery(dto);

        var userContextRole = new UserContextRole
        {
            UserId = query.Dto.UserId,
            ContextId = query.Dto.ContextId,
            ContextType = new ContextType(),
            Role = Role.Owner
        };

        _repositoryMock.Setup(x => x.GetAsync(It.IsAny<Expression<Func<UserContextRole, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(userContextRole);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.Status == ResultStatus.Ok);
        Assert.True(result.Value);
    }

    [Fact]
    public async Task Handle_WithInvalidRequest_ReturnsAccessNotAllowed()
    {
        // Arrange
        var dto = new VerifyUserContextRoleDto
        {
            UserId = 1,
            ContextId = 1,
            ContextType = "SomeContextType",
            Role = Role.Owner
        };
        var query = new VerifyUserContextRoleQuery(dto);

        _repositoryMock.Setup(x => x.GetAsync(It.IsAny<Expression<Func<UserContextRole, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => null);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.Status == ResultStatus.Ok);
        Assert.False(result.Value);
    }
}
