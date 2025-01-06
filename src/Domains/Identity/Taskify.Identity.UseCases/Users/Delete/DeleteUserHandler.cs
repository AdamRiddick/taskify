namespace Taskify.Identity.UseCases.Users.Delete;

using Ardalis.GuardClauses;
using Ardalis.Result;

using Taskify.Identity.Core.UserAggregate;
using Taskify.SharedKernel.Cqrs;
using Taskify.SharedKernel.Data;

public class DeleteUserHandler : ICommandHandler<DeleteUserCommand, Result>
{
    private readonly IRepository<User> _repository;

    public DeleteUserHandler(
        IRepository<User> repository)
    {
        _repository = repository;
    }

    public async Task<Result> Handle(
        DeleteUserCommand request,
        CancellationToken cancellationToken)
    {
        var item = await _repository.GetByIdAsync(request.Id, cancellationToken);
        Guard.Against.Null(item);
        await _repository.DeleteAsync(item, cancellationToken);
        return Result.Success();
    }
}