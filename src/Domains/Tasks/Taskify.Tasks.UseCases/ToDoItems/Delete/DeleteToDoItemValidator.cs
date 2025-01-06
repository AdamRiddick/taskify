namespace Taskify.Tasks.UseCases.ToDoItems.Delete;

using FluentValidation;

using Taskify.SharedKernel.Data;
using Taskify.Tasks.Core.ToDoItemAggregate;

public class DeleteToDoItemValidator : AbstractValidator<DeleteToDoItemCommand>
{
    public DeleteToDoItemValidator(
        IRepository<ToDoItem> repository)
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
