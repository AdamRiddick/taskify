namespace Taskify.Identity.UseCases.UserContextRoles.Delete;

using Ardalis.Result;

using Taskify.Identity.Core.UserContextRoleAggregate;
using Taskify.SharedKernel.Cqrs;
using Taskify.SharedKernel.Data;

public class DeleteUserContextRoleHandler : ICommandHandler<DeleteUserContextRoleCommand, Result>
{
    private readonly IRepository<UserContextRole> _repository;

    public DeleteUserContextRoleHandler(
        IRepository<UserContextRole> repository)
    {
        _repository = repository;
    }

    public async Task<Result> Handle(
        DeleteUserContextRoleCommand request,
        CancellationToken cancellationToken)
    {
        var item = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (item == null)
            return Result.NotFound("UserContextRole not found.");
        await _repository.DeleteAsync(item, cancellationToken);
        return Result.Success();
    }
}