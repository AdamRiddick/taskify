namespace Taskify.Infrastructure.Notifications.Dispatcher;

using FluentValidation;

using Taskify.SharedKernel.Notifications;

public class SendNotificationValidator : AbstractValidator<SendNotificationCommand>
{
    public SendNotificationValidator()
    {
        RuleFor(x => x.Dto).NotNull();
        RuleFor(x => x.Dto.Message).NotEmpty().MaximumLength(1000);
        RuleFor(x => x.Dto.Title).NotEmpty().MaximumLength(100);
    }
}
