namespace Taskify.Identity.UseCases.UserContextRoles.Verify;

using Ardalis.Result;

using Taskify.Identity.Core.UserContextRoleAggregate;
using Taskify.SharedKernel.Cqrs;
using Taskify.SharedKernel.Data;

public class VerifyUserContextRoleHandler : IQueryHandler<VerifyUserContextRoleQuery, Result<bool>>
{
    private readonly IRepository<UserContextRole> _repository;

    public VerifyUserContextRoleHandler(
        IRepository<UserContextRole> repository)
    {
        _repository = repository;
    }

    public async Task<Result<bool>> Handle(
        VerifyUserContextRoleQuery request,
        CancellationToken cancellationToken)
    {
        var userContextRole = await _repository.GetAsync(
            x => x.UserId == request.Dto.UserId
            && (request.Dto.ContextId == null || x.ContextId == request.Dto.ContextId)
            && x.ContextType.Name.Equals(request.Dto.ContextType, StringComparison.InvariantCultureIgnoreCase), cancellationToken);

        var accessAllowed = userContextRole != null && userContextRole.Role >= request.Dto.Role;
        return new Result<bool>(accessAllowed);
    }
}