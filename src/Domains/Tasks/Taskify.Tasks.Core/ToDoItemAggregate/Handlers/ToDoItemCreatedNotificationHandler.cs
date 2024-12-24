namespace Taskify.Tasks.Core.ToDoItemAggregate.Handlers;

using Ardalis.GuardClauses;

using MediatR;

using System.Threading;
using System.Threading.Tasks;

using Taskify.SharedKernel.Email;
using Taskify.Tasks.Core.ToDoItemAggregate.Events;

public class ToDoItemCreatedNotificationHandler : INotificationHandler<ToDoItemCreatedEvent>
{
    private readonly IEmailSender _emailSender;

    public ToDoItemCreatedNotificationHandler(
        IEmailSender emailSender)
    {
        _emailSender = emailSender;
    }

    public Task Handle(ToDoItemCreatedEvent notification, CancellationToken cancellationToken)
    {
        Guard.Against.Null(notification);

        return _emailSender.SendEmailAsync("test@test.com", "test@test.com", "New Item Created", $"\"{notification.Item.Title}\" was created on {notification.DateOccurred}.");
    }
}
