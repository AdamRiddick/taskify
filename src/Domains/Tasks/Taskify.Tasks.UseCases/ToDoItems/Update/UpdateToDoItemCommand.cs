namespace Taskify.Tasks.UseCases.ToDoItems.Update;

using Ardalis.Result;

using Taskify.SharedKernel.Cqrs;
using Taskify.Tasks.UseCases.ToDoItems.Create;

public record UpdateToDoItemCommand(int Id, UpdateToDoItemDto Dto) : ICommand<Result>;
