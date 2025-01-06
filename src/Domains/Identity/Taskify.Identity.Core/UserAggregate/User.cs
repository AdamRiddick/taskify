namespace Taskify.Identity.Core.UserAggregate;

using Taskify.Identity.Core.UserAggregate.Events;
using Taskify.SharedKernel.Data;

public class User : EntityBase, IAggregateRoot
{
    public string EmailAddress { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public void MarkDeleted()
    {
        this.RegisterDomainEvent(new UserDeletedEvent(Id));
    }
}
