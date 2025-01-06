namespace Taskify.Identity.UseCases.Users.Create;

using Ardalis.Result;

using Mapster;

using Taskify.Identity.Core.UserAggregate;
using Taskify.SharedKernel.Cqrs;
using Taskify.SharedKernel.Data;

public class CreateUserHandler : ICommandHandler<CreateUserCommand, Result<int>>
{
    private readonly IRepository<User> _repository;

    public CreateUserHandler(
        IRepository<User> repository)
    {
        _repository = repository;
    }

    public async Task<Result<int>> Handle(
        CreateUserCommand request,
        CancellationToken cancellationToken)
    {
        var newEntity = request.Dto.Adapt<User>();
        var createdItem = await _repository.AddAsync(newEntity, cancellationToken);
        return Result.Created(createdItem.Id);
    }
}