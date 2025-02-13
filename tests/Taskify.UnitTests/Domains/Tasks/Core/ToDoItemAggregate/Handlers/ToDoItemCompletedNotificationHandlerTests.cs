namespace Taskify.UnitTests.Domains.Tasks.Core.ToDoItemAggregate.Handlers;

using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Moq;

using Taskify.SharedKernel.Notifications;
using Taskify.Tasks.Core.ToDoItemAggregate;
using Taskify.Tasks.Core.ToDoItemAggregate.Events;
using Taskify.Tasks.Core.ToDoItemAggregate.Handlers;

using Xunit;

public class ToDoItemCompletedNotificationHandlerTests
{
    [Fact]
    public async Task Handle_ValidNotification_SendsNotificationCommand()
    {
        // Arrange
        var mediatorMock = new Mock<IMediator>();
        var handler = new ToDoItemCompletedNotificationHandler(mediatorMock.Object);
        var notification = new ToDoItemCompletedEvent(new ToDoItem
        {
            Title = "Test ToDo Item",
            AuthorId = 1
        });
        var cancellationToken = new CancellationToken();

        // Act
        await handler.Handle(notification, cancellationToken);

        // Assert
        mediatorMock.Verify(m => m.Publish(
            It.Is<SendNotificationCommand>(command =>
                command.Dto.Message == $"\"{notification.Item.Title}\" was completed on {notification.DateOccurred}."
                && command.Dto.Title == "ToDo Item Completed"
                && command.Dto.UserId == notification.Item.AuthorId),
            cancellationToken), Times.Once);
    }
}
