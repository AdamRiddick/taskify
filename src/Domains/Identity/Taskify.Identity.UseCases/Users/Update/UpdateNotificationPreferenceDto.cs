namespace Taskify.Identity.UseCases.Users.Update
{
    using Taskify.SharedKernel.Notifications;

    public class UpdateNotificationPreferenceDto
    {
        public NotificationChannel NotificationChannel { get; set; } = NotificationChannel.None;

        public NotificationType NotificationType { get; set; } = NotificationType.Marketing;
    }
}
