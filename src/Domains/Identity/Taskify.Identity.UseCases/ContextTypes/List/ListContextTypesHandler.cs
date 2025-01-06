namespace Taskify.Identity.UseCases.ContextTypes.List;

using Ardalis.Result;

using Mapster;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Taskify.SharedKernel.Cqrs;
using Taskify.SharedKernel.Data;
using Taskify.Identity.Core.ContextTypeAggregate;

public class ListContextTypesHandler : IQueryHandler<ListContextTypesQuery, Result<IEnumerable<ListContextTypeDto>>>
{
    private readonly IRepository<ContextType> _repository;

    public ListContextTypesHandler(IRepository<ContextType> repository)
    {
        _repository = repository;
    }

    public async Task<Result<IEnumerable<ListContextTypeDto>>> Handle(ListContextTypesQuery request, CancellationToken cancellationToken)
    {
        var items = await _repository.GetAllAsync();
        var dtos = items.Adapt<List<ListContextTypeDto>>();
        return new Result<IEnumerable<ListContextTypeDto>>(dtos);
    }
}
