namespace Taskify.Web.Tests.Extensions;

using System.Security.Claims;

using Taskify.Web.Extensions;

using Xunit;

public class ClaimsPrincipalExtensionsTests
{
    [Fact]
    public void GetUserId_Should_Return_UserId()
    {
        // Arrange
        var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(
        [
            new Claim(ClaimTypes.NameIdentifier, "123")
        ]));

        // Act
        var userId = claimsPrincipal.GetUserId();

        // Assert
        Assert.Equal(123, userId);
    }

    [Fact]
    public void HasScope_Should_Return_True_If_Scope_Exists()
    {
        // Arrange
        var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim("scope", "read"),
            new Claim("scope", "write")
        }));

        // Act
        var hasScope = claimsPrincipal.HasScope("read");

        // Assert
        Assert.True(hasScope);
    }

    [Fact]
    public void HasScope_Should_Return_False_If_Scope_Does_Not_Exist()
    {
        // Arrange
        var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(
        [
            new Claim("scope", "read"),
            new Claim("scope", "write")
        ]));

        // Act
        var hasScope = claimsPrincipal.HasScope("delete");

        // Assert
        Assert.False(hasScope);
    }
}
