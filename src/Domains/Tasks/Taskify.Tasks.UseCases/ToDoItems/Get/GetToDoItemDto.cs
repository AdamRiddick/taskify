namespace Taskify.Tasks.UseCases.ToDoItems.Get;

using System;
using Taskify.Tasks.Core.ToDoItemAggregate;

public class GetToDoItemDto
{
    public int Id { get; set; }
    public int? AssigneeId { get; set; }
    public int AuthorId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime? DueDate { get; set; }
    public bool IsComplete { get; set; }
    public Priority Priority { get; set; }
}
