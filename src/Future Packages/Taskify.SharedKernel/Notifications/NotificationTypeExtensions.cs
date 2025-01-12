namespace Taskify.SharedKernel.Notifications;

public static class NotificationTypeExtensions
{
    public static bool HasFlag(this NotificationType value, NotificationType flag)
    {
        return (value & flag) == flag;
    }
}
