namespace Taskify.Identity.Core.UserAggregate.Events;

using Taskify.SharedKernel.Events;

public class UserDeletedEvent(int id) : DomainEventBase
{
    public int Id { get; } = id;
}
