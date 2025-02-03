namespace Taskify.UnitTests.TestHelpers.Objects;

using Taskify.SharedKernel.Events;

public class TestEntityWithDomainEvents : HasDomainEventsBase
{
    public int Id { get; set; }

    public void RegisterEvent(DomainEventBase domainEvent)
    {
        this.RegisterDomainEvent(domainEvent);
    }

    public void ClearEvents()
    {
        this.ClearDomainEvents();
    }
}