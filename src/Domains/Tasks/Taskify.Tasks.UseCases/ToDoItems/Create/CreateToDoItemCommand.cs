namespace Taskify.Tasks.UseCases.ToDoItems.Create;

using Ardalis.Result;

using Taskify.SharedKernel.Cqrs;

public record CreateToDoItemCommand(CreateToDoItemDto Dto) : ICommand<Result<int>>;
