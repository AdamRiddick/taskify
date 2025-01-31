namespace Taskify.Tasks.Core.Tests.ToDoItemAggregate.Handlers;

using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Moq;

using Taskify.SharedKernel.Notifications;
using Taskify.Tasks.Core.ToDoItemAggregate;
using Taskify.Tasks.Core.ToDoItemAggregate.Events;
using Taskify.Tasks.Core.ToDoItemAggregate.Handlers;

using Xunit;
public class ToDoItemCreatedNotificationHandlerTests
{
    [Fact]
    public async Task Handle_ValidNotification_SendsNotificationCommand()
    {
        // Arrange
        var mediatorMock = new Mock<IMediator>();
        var handler = new ToDoItemCreatedNotificationHandler(mediatorMock.Object);
        var notification = new ToDoItemCreatedEvent(new ToDoItem
        {
            Title = "Test ToDo Item",
            AuthorId = 1
        });
        var cancellationToken = new CancellationToken();

        // Act
        await handler.Handle(notification, cancellationToken);

        // Assert
        mediatorMock.Verify(m => m.Publish(
            It.Is<SendNotificationCommand>(c =>
                c.Dto.Message == $"\"{notification.Item.Title}\" was created on {notification.DateOccurred}." &&
                c.Dto.Title == "New ToDo Item Created" &&
                c.Dto.UserId == notification.Item.AuthorId
            ),
            cancellationToken
        ), Times.Once);
    }
}
