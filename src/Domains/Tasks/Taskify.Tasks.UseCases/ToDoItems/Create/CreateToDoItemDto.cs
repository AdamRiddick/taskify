namespace Taskify.Tasks.UseCases.ToDoItems.Create;

using System;
using Taskify.Tasks.Core.ToDoItemAggregate;

public class CreateToDoItemDto
{
    public int? AssigneeId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime? DueDate { get; set; }
    public Priority Priority { get; set; }
}
