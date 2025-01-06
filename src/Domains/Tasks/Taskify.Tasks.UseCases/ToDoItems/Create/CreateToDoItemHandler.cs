namespace Taskify.Tasks.UseCases.ToDoItems.Create;

using Ardalis.Result;

using Mapster;

using Taskify.SharedKernel.Cqrs;
using Taskify.SharedKernel.Data;
using Taskify.Tasks.Core.ToDoItemAggregate;

public class CreateToDoItemHandler : ICommandHandler<CreateToDoItemCommand, Result<int>>
{
    private readonly IRepository<ToDoItem> _repository;

    public CreateToDoItemHandler(IRepository<ToDoItem> repository)
    {
        _repository = repository;
    }

    public async Task<Result<int>> Handle(
        CreateToDoItemCommand request,
        CancellationToken cancellationToken)
    {
        var newEntity = request.Dto.Adapt<ToDoItem>();
        newEntity.MarkNew();
        var createdItem = await _repository.AddAsync(newEntity, cancellationToken);
        return Result.Success(createdItem.Id);
    }
}