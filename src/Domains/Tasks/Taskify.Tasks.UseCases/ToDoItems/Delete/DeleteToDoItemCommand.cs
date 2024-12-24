namespace Taskify.Tasks.UseCases.ToDoItems.Delete;

using Ardalis.Result;

using Taskify.SharedKernel.Cqrs;

public record DeleteToDoItemCommand(int Id) : ICommand<Result>;
