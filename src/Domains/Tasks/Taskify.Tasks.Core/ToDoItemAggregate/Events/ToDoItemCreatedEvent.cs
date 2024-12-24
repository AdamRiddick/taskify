namespace Taskify.Tasks.Core.ToDoItemAggregate.Events;

using Taskify.SharedKernel.Events;

public class ToDoItemCreatedEvent(ToDoItem item) : DomainEventBase
{
    public ToDoItem Item { get; } = item;
}
