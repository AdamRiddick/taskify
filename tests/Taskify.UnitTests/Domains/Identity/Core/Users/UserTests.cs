namespace Taskify.UnitTests.Domains.Identity.Core.Users;

using Taskify.Identity.Core.UserAggregate;
using Taskify.Identity.Core.UserAggregate.Events;

using Xunit;

public class UserTests
{
    public class MarkDeletedTests
    {
        [Fact]
        public void ShouldRaiseUserDeletedEvent()
        {
            // Arrange
            var user = new User();

            // Act
            user.MarkDeleted();

            // Assert
            Assert.Single(user.DomainEvents);
            Assert.IsType<UserDeletedEvent>(user.DomainEvents.First());
        }
    }
}
