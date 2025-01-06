namespace Taskify.Tasks.UseCases.ToDoItems.Update;

using FluentValidation;

using Taskify.SharedKernel.Data;
using Taskify.Tasks.Core.ToDoItemAggregate;

public class UpdateToDoItemCommandValidator : AbstractValidator<UpdateToDoItemCommand>
{
    public UpdateToDoItemCommandValidator(
        IReadRepository<ToDoItem> repository)
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Dto).NotNull();
        RuleFor(x => x.Dto.Title).NotEmpty().MaximumLength(255);
        RuleFor(x => x.Dto.Description).MaximumLength(4000);
        RuleFor(x => x.Dto.Priority).IsInEnum();

        RuleFor(x => x)
            .MustAsync(async (x, token) =>
            {
                var existingEntity = await repository.GetByIdAsync(x.Id, token);
                return existingEntity != null;
            });
    }
}
