namespace Taskify.Tasks.UseCases.ToDoItems.MarkToDoItemComplete;

using Ardalis.Result;

using Taskify.SharedKernel.Cqrs;

public record MarkToDoItemCompleteCommand(int Id) : ICommand<Result>;
