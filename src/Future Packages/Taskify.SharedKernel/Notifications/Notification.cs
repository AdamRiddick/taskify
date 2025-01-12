namespace Taskify.SharedKernel.Notifications;

using Taskify.SharedKernel.Data;

public class Notification : EntityBase, IAggregateRoot
{
    public string Message { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public int UserId { get; set; }
}
