namespace Taskify.Tasks.Core.ToDoItemAggregate.Handlers;

using Ardalis.GuardClauses;

using MediatR;

using System.Threading;
using System.Threading.Tasks;

using Taskify.SharedKernel.Notifications;
using Taskify.Tasks.Core.ToDoItemAggregate.Events;

public class ToDoItemCompletedNotificationHandler : INotificationHandler<ToDoItemCompletedEvent>
{
    private readonly IMediator _mediator;

    public ToDoItemCompletedNotificationHandler(
        IMediator mediator)
    {
        _mediator = mediator;
    }

    public Task Handle(ToDoItemCompletedEvent notification, CancellationToken cancellationToken)
    {
        Guard.Against.Null(notification);

        return _mediator.Publish(new SendNotificationCommand(
            new Notification
            {
                Message = $"\"{notification.Item.Title}\" was completed on {notification.DateOccurred}.",
                Title = "ToDo Item Completed",
                UserId = notification.Item.AuthorId
            }));
    }
}
