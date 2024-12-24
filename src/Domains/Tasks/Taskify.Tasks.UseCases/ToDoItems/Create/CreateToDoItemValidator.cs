namespace Taskify.Tasks.UseCases.ToDoItems.Create;

using FluentValidation;

public class CreateToDoItemCommandValidator : AbstractValidator<CreateToDoItemCommand>
{
    public CreateToDoItemCommandValidator()
    {
        RuleFor(x => x.Dto).NotNull();
        RuleFor(x => x.Dto.Title).NotEmpty().MaximumLength(255);
        RuleFor(x => x.Dto.Description).MaximumLength(4000);
    }
}
