namespace Taskify.SharedKernel.Notifications;

using System;

[Flags]
public enum NotificationType
{
    None = 0,
    Email = 1 << 0,  // 1
    SMS = 1 << 1,    // 2
    Push = 1 << 2,   // 4
    WhatsApp = 1 << 3 // 8
}
