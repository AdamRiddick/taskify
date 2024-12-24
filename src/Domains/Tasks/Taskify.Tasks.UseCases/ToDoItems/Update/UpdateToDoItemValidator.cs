namespace Taskify.Tasks.UseCases.ToDoItems.Update;

using FluentValidation;

public class UpdateToDoItemCommandValidator : AbstractValidator<UpdateToDoItemCommand>
{
    public UpdateToDoItemCommandValidator()
    {
        RuleFor(x => x.Dto).NotNull();
        RuleFor(x => x.Dto.Title).NotEmpty().MaximumLength(255);
        RuleFor(x => x.Dto.Description).MaximumLength(4000);
    }
}
