namespace Taskify.UnitTests.Future_Packages.Taskify.Infrastructure.Notifications.Dispatcher;

using System.Threading;
using System.Threading.Tasks;

using global::Taskify.Infrastructure.Notifications.Dispatcher;
using global::Taskify.SharedKernel.Data;
using global::Taskify.SharedKernel.Notifications;

using Moq;

using Xunit;

public class SendNotificationHandlerTests
{
    private readonly Mock<IRepository<Notification>> _repositoryMock;
    private readonly SendNotificationHandler _handler;

    public SendNotificationHandlerTests()
    {
        _repositoryMock = new Mock<IRepository<Notification>>();
        _handler = new SendNotificationHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task Handle_Should_Return_True_When_Notification_Is_Saved()
    {
        // Arrange
        var command = new SendNotificationCommand(new Notification());

        _repositoryMock
            .Setup(repo => repo.AddAsync(It.IsAny<Notification>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(command.Dto);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result);
        _repositoryMock.Verify(repo => repo.AddAsync(command.Dto, It.IsAny<CancellationToken>()), Times.Once);
    }
}

