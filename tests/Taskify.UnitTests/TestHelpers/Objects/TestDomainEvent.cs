namespace Taskify.UnitTests.TestHelpers.Objects;

using Taskify.SharedKernel.Events;

public class TestDomainEvent : DomainEventBase
{
    public Guid Guid { get; } = Guid.NewGuid();
}