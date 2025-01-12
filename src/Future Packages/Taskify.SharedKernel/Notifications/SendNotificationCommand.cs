namespace Taskify.SharedKernel.Notifications;

using Taskify.SharedKernel.Cqrs;

public record SendNotificationCommand(Notification Dto) : ICommand<bool>;
