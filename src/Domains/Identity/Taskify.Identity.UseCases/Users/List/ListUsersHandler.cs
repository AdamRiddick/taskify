namespace Taskify.Identity.UseCases.Users.List;

using Ardalis.Result;

using Mapster;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Taskify.SharedKernel.Cqrs;
using Taskify.SharedKernel.Data;
using Taskify.Identity.Core.ContextTypeAggregate;

public class ListUsersHandler : IQueryHandler<ListUsersQuery, Result<IEnumerable<ListUsersDto>>>
{
    private readonly IRepository<ContextType> _repository;

    public ListUsersHandler(IRepository<ContextType> repository)
    {
        _repository = repository;
    }

    public async Task<Result<IEnumerable<ListUsersDto>>> Handle(
        ListUsersQuery request,
        CancellationToken cancellationToken)
    {
        var items = await _repository.GetAllAsync();
        var dtos = items.Adapt<List<ListUsersDto>>();
        return new Result<IEnumerable<ListUsersDto>>(dtos);
    }
}
