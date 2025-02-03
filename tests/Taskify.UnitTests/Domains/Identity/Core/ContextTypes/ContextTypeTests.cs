namespace Taskify.UnitTests.Domains.Identity.Core.ContextTypes;

using Taskify.Identity.Core.ContextTypeAggregate;
using Taskify.Identity.Core.ContextTypeAggregate.Events;

using Xunit;

public class ContextTypeTests
{
    public class MarkDeletedTests
    {
        [Fact]
        public void ShouldRaiseContextTypeDeletedEvent()
        {
            // Arrange
            var contextType = new ContextType();

            // Act
            contextType.MarkDeleted();

            // Assert
            Assert.Single(contextType.DomainEvents);
            Assert.IsType<ContextTypeDeletedEvent>(contextType.DomainEvents.First());
        }
    }
}
