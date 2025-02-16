﻿namespace Taskify.Identity.UseCases.Users.Update;

using Ardalis.Result;

using Mapster;

using Taskify.Identity.Core.UserAggregate;
using Taskify.SharedKernel.Cqrs;
using Taskify.SharedKernel.Data;

public class UpdateUserHandler : ICommandHandler<UpdateUserCommand, Result>
{
    private readonly IRepository<User> _repository;

    public UpdateUserHandler(IRepository<User> repository)
    {
        _repository = repository;
    }

    public async Task<Result> Handle(
        UpdateUserCommand request,
        CancellationToken cancellationToken)
    {
        var existingItem = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (existingItem == null)
            return Result.NotFound("User not found.");

        request.Dto.Adapt(existingItem);
        await _repository.UpdateAsync(existingItem, cancellationToken);
        return Result.Success();
    }
}