namespace Taskify.Tasks.UseCases.ToDoItems.List;

using System;
using Taskify.Tasks.Core.ToDoItemAggregate;

public class ListToDoItemDto
{
    public int? AssigneeId { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTime? DueDate { get; set; }
    public int Id { get; set; }
    public Priority Priority { get; set; }
    public string Title { get; set; } = string.Empty;

}
