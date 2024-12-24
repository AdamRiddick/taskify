namespace Taskify.Tasks.UseCases.ToDoItems.Get;

using Ardalis.Result;
using Taskify.SharedKernel.Cqrs;

public record GetToDoItemQuery(int Id) : IQuery<Result<GetToDoItemDto>>;
