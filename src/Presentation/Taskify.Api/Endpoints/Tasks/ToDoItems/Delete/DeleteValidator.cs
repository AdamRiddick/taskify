namespace Taskify.Api.Endpoints.Tasks.ToDoItems.Delete;
using Taskify.Tasks.UseCases.ToDoItems.Delete;

using FluentValidation;

using Taskify.SharedKernel.Data;
using Taskify.Tasks.Core.ToDoItemAggregate;

public class DeleteValidator : AbstractValidator<DeleteToDoItemCommand>
{
    public DeleteValidator(
        IRepository<ToDoItem> repository)
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
