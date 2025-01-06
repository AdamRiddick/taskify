namespace Taskify.Identity.Core.ContextTypeAggregate.Events;

using Taskify.SharedKernel.Events;

public class ContextTypeDeletedEvent(int id) : DomainEventBase
{
    public int Id { get; } = id;
}
