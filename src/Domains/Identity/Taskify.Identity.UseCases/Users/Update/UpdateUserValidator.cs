namespace Taskify.Identity.UseCases.Users.Update;

using FluentValidation;

using Taskify.SharedKernel.Data;
using Taskify.Identity.Core.UserAggregate;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator(
        IReadRepository<User> repository)
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Dto).NotNull();
        RuleFor(x => x.Dto.EmailAddress)
            .NotNull()
            .MaximumLength(254)
            .EmailAddress();
        RuleFor(x => x.Dto.Name)
            .NotNull()
            .MinimumLength(3)
            .MaximumLength(100);

        RuleFor(x => x)
            .MustAsync(async (x, token) =>
            {
                var existingEntity = await repository.GetByIdAsync(x.Id, token);
                return existingEntity != null;
            });
    }
}
