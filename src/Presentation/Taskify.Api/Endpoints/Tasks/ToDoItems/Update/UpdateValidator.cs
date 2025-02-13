namespace Taskify.Api.Endpoints.Tasks.ToDoItems.Update;
using Taskify.Tasks.UseCases.ToDoItems.Update;

using FluentValidation;

using Taskify.SharedKernel.Data;
using Taskify.Tasks.Core.ToDoItemAggregate;
using Taskify.Identity.Core.UserAggregate;

public class UpdateValidator : AbstractValidator<UpdateToDoItemCommand>
{
    public UpdateValidator(
        IReadRepository<ToDoItem> repository,
        IReadRepository<User> userRepository)
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
            })
            .WithMessage("Entity does not exist.");

        RuleFor(x => x.Dto.AssigneeId)
            .MustAsync(async (x, token) =>
            {
                if (x == null)
                {
                    return true;
                }

                var user = await userRepository.GetByIdAsync(x.Value, token);
                return user != null;
            })
            .WithMessage("Assignee does not exist.");
    }
}
