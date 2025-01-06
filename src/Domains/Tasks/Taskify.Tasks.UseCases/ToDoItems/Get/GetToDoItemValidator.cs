namespace Taskify.Tasks.UseCases.ToDoItems.Get;

using FluentValidation;

using Taskify.SharedKernel.Data;
using Taskify.Tasks.Core.ToDoItemAggregate;

public class GetToDoItemValidator : AbstractValidator<GetToDoItemQuery>
{
    public GetToDoItemValidator(
        IReadRepository<ToDoItem> repository)
    {
        RuleFor(x => x.Id).NotEmpty();

        RuleFor(x => x)
            .MustAsync(async (x, token) =>
            {
                var existingEntity = await repository.GetByIdAsync(x.Id, token);
                return existingEntity != null;
            });
    }
}
