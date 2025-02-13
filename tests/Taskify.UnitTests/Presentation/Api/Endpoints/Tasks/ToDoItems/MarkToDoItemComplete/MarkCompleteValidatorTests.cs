namespace Taskify.UnitTests.Presentation.Api.Endpoints.Tasks.ToDoItems.MarkToDoItemComplete;

using FluentValidation.TestHelper;

using Moq;

using System.Threading;
using System.Threading.Tasks;

using Taskify.Tasks.Core.ToDoItemAggregate;
using Taskify.SharedKernel.Data;

using Xunit;
using Taskify.Api.Endpoints.Tasks.ToDoItems.MarkToDoItemComplete;
using Taskify.Tasks.UseCases.ToDoItems.MarkToDoItemComplete;

public class MarkCompleteValidatorTests
{
    private readonly Mock<IReadRepository<ToDoItem>> _repositoryMock;
    private readonly MarkCompleteValidator _validator;

    public MarkCompleteValidatorTests()
    {
        _repositoryMock = new Mock<IReadRepository<ToDoItem>>();
        _validator = new MarkCompleteValidator(_repositoryMock.Object);
    }

    [Fact]
    public async Task ShouldHaveErrorWhenEntityDoesNotExist()
    {
        // Arrange
        var command = new MarkToDoItemCompleteCommand(1);
        _repositoryMock.Setup(x => x.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(()=> null);

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x)
            .WithErrorMessage("Entity does not exist.");
    }

    [Fact]
    public async Task ShouldNotHaveErrorWhenEntityExists()
    {
        // Arrange
        var command = new MarkToDoItemCompleteCommand(1);
        _repositoryMock.Setup(x => x.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ToDoItem());

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x);
    }
}
