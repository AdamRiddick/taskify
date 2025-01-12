﻿namespace Taskify.Identity.UseCases.Users.Create
{
    using Taskify.SharedKernel.Notifications;

    public class NotificationPreferenceDto
    {
        public NotificationChannel NotificationChannel { get; set; } = NotificationChannel.None;

        public NotificationType NotificationType { get; set; } = NotificationType.Marketing;
    }
}
