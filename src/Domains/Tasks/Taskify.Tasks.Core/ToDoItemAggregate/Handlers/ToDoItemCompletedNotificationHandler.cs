namespace Taskify.Tasks.Core.ToDoItemAggregate.Handlers;

using Ardalis.GuardClauses;

using MediatR;

using System.Threading;
using System.Threading.Tasks;

using Taskify.SharedKernel.Email;
using Taskify.Tasks.Core.ToDoItemAggregate.Events;

public class ToDoItemCompletedNotificationHandler : INotificationHandler<ToDoItemCompletedEvent>
{
    private readonly IEmailSender _emailSender;

    public ToDoItemCompletedNotificationHandler(
        IEmailSender emailSender)
    {
        _emailSender = emailSender;
    }

    public Task Handle(ToDoItemCompletedEvent notification, CancellationToken cancellationToken)
    {
        Guard.Against.Null(notification);

        return _emailSender.SendEmailAsync("test@test.com", "test@test.com", "Item Complete", $"\"{notification.Item.Title}\" was completed on {notification.DateOccurred}.");
    }
}
