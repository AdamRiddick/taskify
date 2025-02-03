namespace Taskify.UnitTests.Future_Packages.Taskify.SharedKernel.Behaviour;

using System.Threading;
using System.Threading.Tasks;

using global::Taskify.SharedKernel.Behaviour;
using global::Taskify.UnitTests.TestHelpers.Objects;

using MediatR;

using Microsoft.Extensions.Logging;

using Moq;

using Xunit;

public class LoggingBehaviourTests
{
    private readonly Mock<ILogger<Mediator>> _loggerMock;
    private readonly LoggingBehaviour<TestRequest, TestResponse> _loggingBehaviour;
    private readonly TestRequest _testRequest;
    private readonly TestResponse _testResponse;

    public LoggingBehaviourTests()
    {
        _loggerMock = new Mock<ILogger<Mediator>>();
        _loggingBehaviour = new LoggingBehaviour<TestRequest, TestResponse>(_loggerMock.Object);
        _testRequest = new TestRequest { Id = 42, Name = "Test" };
        _testResponse = new TestResponse { Success = true };
    }

    [Fact]
    public async Task Handle_Should_Log_Request_And_Response_When_Logging_Enabled()
    {
        // Arrange
        _loggerMock.Setup(x => x.IsEnabled(LogLevel.Information)).Returns(true); // Enable logging (Information level)

        _loggerMock.Setup(l => l.Log(
            It.Is<LogLevel>(level => level == LogLevel.Information), // Ensure it's logging at Information level
            It.IsAny<EventId>(),
            It.IsAny<It.IsAnyType>(),  // Matches any logged message (e.g., FormattedLogValues)
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()))
        .Verifiable();

        Task<TestResponse> next() => Task.FromResult(_testResponse);

        // Act
        var result = await _loggingBehaviour.Handle(_testRequest, next, CancellationToken.None);

        // Assert
        Assert.Equal(_testResponse, result);

        // Verify that logging happened at least once
        _loggerMock.Verify(
            l => l.Log(
                It.Is<LogLevel>(level => level == LogLevel.Information), // Ensure it's an Information log
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),  // Matches any logged message
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.AtLeastOnce);
    }

    [Fact]
    public async Task Handle_Should_Not_Log_When_Logging_Disabled()
    {
        // Arrange
        _loggerMock.Setup(x => x.IsEnabled(LogLevel.Information)).Returns(false);  // Disable logging (Information level)
        _loggerMock.Setup(l => l.Log(It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<object>(), It.IsAny<Exception>(), It.IsAny<Func<object, Exception?, string>>()))
                   .Verifiable();  // Ensure logging method is called

        Task<TestResponse> next() => Task.FromResult(_testResponse);

        // Act
        var result = await _loggingBehaviour.Handle(_testRequest, next, CancellationToken.None);

        // Assert
        Assert.Equal(_testResponse, result);
        _loggerMock.Verify(
            l => l.Log(LogLevel.Information, It.IsAny<EventId>(), It.IsAny<object>(), null, It.IsAny<Func<object, Exception?, string>>()),
            Times.Never);  // Ensures that Log was never called for Information level when logging is disabled
    }

    [Fact]
    public async Task Handle_Should_Call_Next_Delegate()
    {
        // Arrange
        var nextMock = new Mock<RequestHandlerDelegate<TestResponse>>();
        nextMock.Setup(n => n()).ReturnsAsync(_testResponse);

        _loggerMock.Setup(l => l.Log(It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<object>(), It.IsAny<Exception>(), It.IsAny<Func<object, Exception?, string>>()))
                   .Verifiable();  // Ensure logging method is called

        // Act
        var result = await _loggingBehaviour.Handle(_testRequest, nextMock.Object, CancellationToken.None);

        // Assert
        Assert.Equal(_testResponse, result);
        nextMock.Verify(n => n(), Times.Once);  // Ensures that the next delegate is called
    }
}
