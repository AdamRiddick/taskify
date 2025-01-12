namespace Taskify.SharedKernel.Notifications;

using System;

[Flags]
public enum NotificationChannel
{
    None = 0,
    InApp = 1 << 0,  // 1
    Email = 1 << 1,  // 2
    SMS = 1 << 2,    // 4
    Push = 1 << 3,   // 8
    WhatsApp = 1 << 4 // 16
}
