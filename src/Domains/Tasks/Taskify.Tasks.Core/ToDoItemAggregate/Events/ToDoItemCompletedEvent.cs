namespace Taskify.Tasks.Core.ToDoItemAggregate.Events;

using Taskify.SharedKernel.Events;

public class ToDoItemCompletedEvent(ToDoItem item) : DomainEventBase
{
    public ToDoItem Item { get; } = item;
}
