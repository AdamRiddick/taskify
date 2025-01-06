namespace Taskify.Tasks.UseCases.ToDoItems.MarkToDoItemComplete;

using Ardalis.GuardClauses;
using Ardalis.Result;
using Taskify.SharedKernel.Cqrs;
using Taskify.SharedKernel.Data;
using Taskify.Tasks.Core.ToDoItemAggregate;

public class MarkToDoItemCompleteHandler : ICommandHandler<MarkToDoItemCompleteCommand, Result>
{
    private readonly IRepository<ToDoItem> _repository;

    public MarkToDoItemCompleteHandler(IRepository<ToDoItem> repository)
    {
        _repository = repository;
    }

    public async Task<Result> Handle(
        MarkToDoItemCompleteCommand request,
        CancellationToken cancellationToken)
    {
        var item = await _repository.GetByIdAsync(request.Id, cancellationToken);
        Guard.Against.Null(item);
        item.MarkComplete();
        await _repository.UpdateAsync(item, cancellationToken);
        return Result.Success();
    }
}