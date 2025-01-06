namespace Taskify.Tasks.UseCases.ToDoItems.Update;

using Ardalis.Result;

using Taskify.SharedKernel.Cqrs;

public record UpdateToDoItemCommand(int Id, UpdateToDoItemDto Dto) : ICommand<Result>;
