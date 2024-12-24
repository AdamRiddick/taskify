namespace Taskify.Tasks.UseCases.ToDoItems.List;

using Ardalis.Result;

using Mapster;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Taskify.SharedKernel.Cqrs;
using Taskify.SharedKernel.Data;
using Taskify.Tasks.Core.ToDoItemAggregate;

public class ListToDoItemsHandler : IQueryHandler<ListToDoItemsQuery, Result<IEnumerable<ListToDoItemDto>>>
{
    private readonly IRepository<ToDoItem> _repository;

    public ListToDoItemsHandler(IRepository<ToDoItem> repository)
    {
        _repository = repository;
    }

    public async Task<Result<IEnumerable<ListToDoItemDto>>> Handle(ListToDoItemsQuery request, CancellationToken cancellationToken)
    {
        var items = await _repository.GetAllAsync();
        var dtos = items.Adapt<List<ListToDoItemDto>>();
        return new Result<IEnumerable<ListToDoItemDto>>(dtos);
    }
}
