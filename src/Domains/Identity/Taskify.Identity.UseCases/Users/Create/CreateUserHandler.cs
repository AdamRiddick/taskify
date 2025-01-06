namespace Taskify.Identity.UseCases.UserContextRoles.Create;

using Ardalis.Result;

using Mapster;

using Taskify.Identity.Core.UserContextRoleAggregate;
using Taskify.SharedKernel.Cqrs;
using Taskify.SharedKernel.Data;

public class CreateUserContextRoleHandler : ICommandHandler<CreateUserContextRoleCommand, Result<int>>
{
    private readonly IRepository<UserContextRole> _repository;

    public CreateUserContextRoleHandler(
        IRepository<UserContextRole> repository)
    {
        _repository = repository;
    }

    public async Task<Result<int>> Handle(
        CreateUserContextRoleCommand request,
        CancellationToken cancellationToken)
    {
        var newEntity = request.Dto.Adapt<UserContextRole>();
        var createdItem = await _repository.AddAsync(newEntity, cancellationToken);
        return Result.Created(createdItem.Id);
    }
}