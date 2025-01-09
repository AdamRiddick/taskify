namespace Taskify.Web.Endpoints.Tasks.ToDoItems.Create;

using Taskify.Tasks.UseCases.ToDoItems.Create;

using FluentValidation;
using Taskify.SharedKernel.Data;
using Taskify.Identity.Core.UserAggregate;

public class CreateValidator : AbstractValidator<CreateToDoItemCommand>
{
    public CreateValidator(
        IReadRepository<User> userRepository)
    {
        RuleFor(x => x.Dto).NotNull();
        RuleFor(x => x.Dto.Title).NotEmpty().MaximumLength(255);
        RuleFor(x => x.Dto.Description).MaximumLength(4000);
        RuleFor(x => x.Dto.Priority).IsInEnum();

        RuleFor(x => x.Dto.AssigneeId)
            .MustAsync(async (x, token) =>
            {
                if (x == null)
                {
                    return true;
                }

                var user = await userRepository.GetByIdAsync(x.Value, token);
                return user != null;
            });
    }
}
