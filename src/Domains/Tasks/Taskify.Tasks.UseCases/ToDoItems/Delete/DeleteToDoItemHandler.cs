namespace Taskify.Tasks.UseCases.ToDoItems.Delete;

using Ardalis.Result;
using Taskify.SharedKernel.Cqrs;
using Taskify.SharedKernel.Data;
using Taskify.Tasks.Core.ToDoItemAggregate;

public class DeleteToDoItemHandler : ICommandHandler<DeleteToDoItemCommand, Result>
{
    private readonly IRepository<ToDoItem> _repository;

    public DeleteToDoItemHandler(IRepository<ToDoItem> repository)
    {
        _repository = repository;
    }

    public async Task<Result> Handle(
        DeleteToDoItemCommand request,
        CancellationToken cancellationToken)
    {
        var item = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (item == null)
            return Result.NotFound("Item not found.");

        await _repository.DeleteAsync(item, cancellationToken);
        return Result.Success();
    }
}