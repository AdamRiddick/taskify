namespace Taskify.Tasks.UseCases.ToDoItems.Get;

using Ardalis.Result;

using Mapster;

using System.Threading;
using System.Threading.Tasks;

using Taskify.SharedKernel.Cqrs;
using Taskify.SharedKernel.Data;
using Taskify.Tasks.Core.ToDoItemAggregate;
public class GetToDoItemHandler : IQueryHandler<GetToDoItemQuery, Result<GetToDoItemDto>>
{
    private readonly IRepository<ToDoItem> _repository;

    public GetToDoItemHandler(IRepository<ToDoItem> repository)
    {
        _repository = repository;
    }

    public async Task<Result<GetToDoItemDto>> Handle(GetToDoItemQuery request, CancellationToken cancellationToken)
    {
        var item = await _repository.GetByIdAsync(request.Id);
        if (item == null) return Result<GetToDoItemDto>.NotFound();

        var dto = item.Adapt<GetToDoItemDto>();
        return new Result<GetToDoItemDto>(dto);
    }
}
