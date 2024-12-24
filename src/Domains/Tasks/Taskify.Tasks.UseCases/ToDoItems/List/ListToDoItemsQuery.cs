namespace Taskify.Tasks.UseCases.ToDoItems.List;

using Ardalis.Result;
using Taskify.SharedKernel.Cqrs;

public record ListToDoItemsQuery() : IQuery<Result<IEnumerable<ListToDoItemDto>>>;
