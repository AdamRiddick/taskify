namespace Taskify.Identity.UseCases.ContextTypes.Create;

using Ardalis.Result;

using Mapster;

using Taskify.Identity.Core.ContextTypeAggregate;
using Taskify.SharedKernel.Cqrs;
using Taskify.SharedKernel.Data;

public class CreateContextTypeHandler : ICommandHandler<CreateContextTypeCommand, Result<int>>
{
    private readonly IRepository<ContextType> _repository;

    public CreateContextTypeHandler(IRepository<ContextType> repository)
    {
        _repository = repository;
    }

    public async Task<Result<int>> Handle(
        CreateContextTypeCommand request,
        CancellationToken cancellationToken)
    {
        var newEntity = request.Dto.Adapt<ContextType>();
        var createdItem = await _repository.AddAsync(newEntity, cancellationToken);
        return Result.Created(createdItem.Id);
    }
}