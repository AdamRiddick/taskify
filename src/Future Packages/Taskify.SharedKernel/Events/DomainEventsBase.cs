namespace Taskify.SharedKernel.Events;

using MediatR;

public abstract class DomainEventBase : INotification
{
    public DateTime DateOccurred { get; protected set; } = DateTime.UtcNow;
}
