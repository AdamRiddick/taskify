namespace Taskify.Api.Endpoints.Tasks.ToDoItems.Get;
using Taskify.Tasks.UseCases.ToDoItems.Get;

using FluentValidation;

using Taskify.SharedKernel.Data;
using Taskify.Tasks.Core.ToDoItemAggregate;

public class GetValidator : AbstractValidator<GetToDoItemQuery>
{
    public GetValidator(
        IReadRepository<ToDoItem> repository)
    {
        RuleFor(x => x.Id).NotEmpty();

        RuleFor(x => x)
            .MustAsync(async (x, token) =>
            {
                var existingEntity = await repository.GetByIdAsync(x.Id, token);
                return existingEntity != null;
            })
            .WithMessage("Entity does not exist.");
    }
}
