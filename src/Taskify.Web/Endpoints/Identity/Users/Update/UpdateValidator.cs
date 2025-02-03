namespace Taskify.Web.Endpoints.Identity.Users.Update;
using Taskify.Identity.UseCases.Users.Update;

using FluentValidation;

using Taskify.SharedKernel.Data;
using Taskify.Identity.Core.UserAggregate;

public class UpdateValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateValidator(
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

        // Ensure that the user does not have duplicate notification preferences
        RuleForEach(x => x.Dto.NotificationPreferences)
            .Must((user, preference) => !user.Dto.NotificationPreferences.Any(np => np.NotificationType == preference.NotificationType
                   && np.NotificationChannel == preference.NotificationChannel));

        RuleFor(x => x)
            .MustAsync(async (x, token) =>
            {
                var existingEntity = await repository.GetByIdAsync(x.Id, token);
                return existingEntity != null;
            })
            .WithMessage("Entity does not exist.");
    }
}
