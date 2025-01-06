namespace Taskify.Tasks.UseCases.ToDoItems.Update;

using System;

using Taskify.Tasks.Core.ToDoItemAggregate;

public class UpdateToDoItemDto
{
    public int? AssigneeId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime? DueDate { get; set; }
    public Priority Priority { get; set; }
}
