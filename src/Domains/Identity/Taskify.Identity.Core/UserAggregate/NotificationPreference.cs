namespace Taskify.Identity.Core.UserAggregate
{
    using Taskify.SharedKernel.Data;
    using Taskify.SharedKernel.Notifications;

    public class NotificationPreference : EntityBase
    {
        public NotificationChannel NotificationChannel { get; set; } = NotificationChannel.None;

        public NotificationType NotificationType { get; set; } = NotificationType.Marketing;

        public User User { get; set; } = default!;

        public int UserId { get; set; }
    }
}
