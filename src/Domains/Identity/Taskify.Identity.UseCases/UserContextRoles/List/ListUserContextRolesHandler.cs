namespace Taskify.Identity.UseCases.UserContextRoles.List;

using Ardalis.Result;

using Mapster;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Taskify.SharedKernel.Cqrs;
using Taskify.SharedKernel.Data;
using Taskify.Identity.Core.ContextTypeAggregate;

public class ListUserContextRolesHandler : IQueryHandler<ListUserContextRolesQuery, Result<IEnumerable<ListUserContextRolesDto>>>
{
    private readonly IRepository<ContextType> _repository;

    public ListUserContextRolesHandler(IRepository<ContextType> repository)
    {
        _repository = repository;
    }

    public async Task<Result<IEnumerable<ListUserContextRolesDto>>> Handle(
        ListUserContextRolesQuery request,
        CancellationToken cancellationToken)
    {
        var items = await _repository.GetAllAsync();
        var dtos = items.Adapt<List<ListUserContextRolesDto>>();
        return new Result<IEnumerable<ListUserContextRolesDto>>(dtos);
    }
}
