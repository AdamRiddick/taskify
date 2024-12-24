namespace Taskify.SharedKernel.Data;

using Taskify.SharedKernel.Events;

public abstract class EntityBase : HasDomainEventsBase, IEntity
{
    public int Id { get; set; }
}
