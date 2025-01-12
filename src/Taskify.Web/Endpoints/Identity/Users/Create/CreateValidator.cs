namespace Taskify.Web.Endpoints.Identity.Users.Create;

using Taskify.Identity.UseCases.Users.Create;

using FluentValidation;

public class CreateValidator : AbstractValidator<CreateUserCommand>
{
    public CreateValidator()
    {
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
    }
}
