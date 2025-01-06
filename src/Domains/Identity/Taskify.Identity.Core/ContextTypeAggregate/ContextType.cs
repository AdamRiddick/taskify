namespace Taskify.Identity.Core.ContextTypeAggregate;

using Taskify.Identity.Core.ContextTypeAggregate.Events;
using Taskify.SharedKernel.Data;

public class ContextType : EntityBase, IAggregateRoot
{
    public string Name { get; set; } = string.Empty;

    public void MarkDeleted()
    {
        this.RegisterDomainEvent(new ContextTypeDeletedEvent(Id));
    }
}