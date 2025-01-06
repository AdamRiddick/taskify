﻿namespace Taskify.Tasks.UseCases.ToDoItems.Update;

using Ardalis.GuardClauses;
using Ardalis.Result;

using Mapster;

using Taskify.SharedKernel.Cqrs;
using Taskify.SharedKernel.Data;
using Taskify.Tasks.Core.ToDoItemAggregate;

public class UpdateToDoItemHandler : ICommandHandler<UpdateToDoItemCommand, Result>
{
    private readonly IRepository<ToDoItem> _repository;

    public UpdateToDoItemHandler(IRepository<ToDoItem> repository)
    {
        _repository = repository;
    }

    public async Task<Result> Handle(
        UpdateToDoItemCommand request,
        CancellationToken cancellationToken)
    {
        var existingItem = await _repository.GetByIdAsync(request.Id, cancellationToken);
        Guard.Against.Null(existingItem);

        request.Dto.Adapt(existingItem);
        await _repository.UpdateAsync(existingItem, cancellationToken);
        return Result.Success();
    }
}