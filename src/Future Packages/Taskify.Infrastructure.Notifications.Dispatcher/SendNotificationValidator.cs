namespace Taskify.Infrastructure.Notifications.Dispatcher;

using FluentValidation;

using Taskify.SharedKernel.Notifications;

public class SendNotificationValidator : AbstractValidator<SendNotificationCommand>
{
    public SendNotificationValidator()
    {
        RuleFor(x => x.Dto).NotNull();
        RuleFor(x => x.Dto.Message)
            .NotEmpty()
            .WithMessage("'Message' must not be empty.")
            .MaximumLength(1000)
            .WithMessage("The length of 'Message' must be 1000 characters or fewer.");
        
        
        RuleFor(x => x.Dto.Title)
            .NotEmpty()
            .WithMessage("'Title' must not be empty.")
            .MaximumLength(100)
            .WithMessage("The length of 'Title' must be 100 characters or fewer.");
    }
}
