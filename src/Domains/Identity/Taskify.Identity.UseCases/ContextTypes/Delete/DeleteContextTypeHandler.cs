namespace Taskify.Identity.UseCases.ContextTypes.Delete;

using Ardalis.Result;

using Taskify.Identity.Core.ContextTypeAggregate;
using Taskify.SharedKernel.Cqrs;
using Taskify.SharedKernel.Data;

public class DeleteContextTypeHandler : ICommandHandler<DeleteContextTypeCommand, Result>
{
    private readonly IRepository<ContextType> _repository;

    public DeleteContextTypeHandler(IRepository<ContextType> repository)
    {
        _repository = repository;
    }

    public async Task<Result> Handle(
        DeleteContextTypeCommand request,
        CancellationToken cancellationToken)
    {
        var item = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if(item == null)
            return Result.NotFound("ContextType not found.");

        item.MarkDeleted();
        await _repository.DeleteAsync(item, cancellationToken);
        return Result.Success();
    }
}