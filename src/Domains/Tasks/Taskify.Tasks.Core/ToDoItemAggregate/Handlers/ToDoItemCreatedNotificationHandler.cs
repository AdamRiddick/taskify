namespace Taskify.Tasks.Core.ToDoItemAggregate.Handlers;

using Ardalis.GuardClauses;

using MediatR;

using System.Threading;
using System.Threading.Tasks;

using Taskify.SharedKernel.Notifications;
using Taskify.Tasks.Core.ToDoItemAggregate.Events;

public class ToDoItemCreatedNotificationHandler : INotificationHandler<ToDoItemCreatedEvent>
{
    private readonly IMediator _mediator;

    public ToDoItemCreatedNotificationHandler(
        IMediator mediator)
    {
        _mediator = mediator;
    }

    public Task Handle(ToDoItemCreatedEvent notification, CancellationToken cancellationToken)
    {
        Guard.Against.Null(notification);

        return _mediator.Publish(new SendNotificationCommand(
            new Notification
            {
                Message = $"\"{notification.Item.Title}\" was created on {notification.DateOccurred}.",
                Title = "New ToDo Item Created",
                UserId = notification.Item.AuthorId
            }));
    }
}
