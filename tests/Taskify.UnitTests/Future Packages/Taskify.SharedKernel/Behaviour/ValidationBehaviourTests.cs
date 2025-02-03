namespace Taskify.SharedKernel.Tests.Behaviour;

using FluentValidation;

using MediatR;

using Moq;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Taskify.SharedKernel.Behaviour;
using Taskify.UnitTests.TestHelpers;
using Taskify.UnitTests.TestHelpers.Objects;

using Xunit;

public class ValidationBehaviourTests
{
    [Fact]
    public async Task Handle_ValidRequest_NoValidationErrors()
    {
        // Arrange
        var validators = new List<IValidator<TestRequest>>();
        var request = new TestRequest();
        var next = new Mock<RequestHandlerDelegate<TestResponse>>();
        var cancellationToken = new CancellationToken();

        var behaviour = new ValidationBehavior<TestRequest, TestResponse>(validators);

        // Act
        var result = await behaviour.Handle(request, next.Object, cancellationToken);

        // Assert
        next.Verify(x => x(), Times.Once);
        Assert.Equal(default, result);
    }

    [Fact]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Arrange
        var validators = new List<IValidator<TestRequest>>
        {
            new TestRequestValidator(shouldFail: true)
        };
        var request = new TestRequest();
        var next = new Mock<RequestHandlerDelegate<TestResponse>>();
        var cancellationToken = new CancellationToken();

        var behaviour = new ValidationBehavior<TestRequest, TestResponse>(validators);

        // Act & Assert
        await Assert.ThrowsAsync<ValidationException>(() => behaviour.Handle(request, next.Object, cancellationToken));
        next.Verify(x => x(), Times.Never);
    }
}
