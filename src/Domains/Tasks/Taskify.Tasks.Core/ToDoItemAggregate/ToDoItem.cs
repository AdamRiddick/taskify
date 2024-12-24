namespace Taskify.Tasks.Core.ToDoItemAggregate;

using Taskify.SharedKernel.Data;
using Taskify.Tasks.Core.ToDoItemAggregate.Events;

public class ToDoItem : EntityBase, IAggregateRoot
{
    public int AuthorId { get; set; }

    public int? AssigneeId { get; set; }

    public string Description { get; set; } = string.Empty;

    public DateTime? DueDate { get; set; }

    public bool IsComplete { get; private set; } = false;

    public Priority Priority { get; set; } = Priority.None;

    public string Title { get; set; } = string.Empty;

    public void MarkComplete()
    {
        if (IsComplete) return;

        IsComplete = true;
        this.RegisterDomainEvent(new ToDoItemCompletedEvent(this));
    }

    public void MarkNew()
    {
        this.RegisterDomainEvent(new ToDoItemCreatedEvent(this));
    }
}
