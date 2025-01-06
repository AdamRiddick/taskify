namespace Taskify.Tasks.UseCases.ToDoItems.MarkToDoItemComplete;

using FluentValidation;

using Taskify.SharedKernel.Data;
using Taskify.Tasks.Core.ToDoItemAggregate;

public class MarkToDoItemCompleteValidator : AbstractValidator<MarkToDoItemCompleteCommand>
{
    public MarkToDoItemCompleteValidator(
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
