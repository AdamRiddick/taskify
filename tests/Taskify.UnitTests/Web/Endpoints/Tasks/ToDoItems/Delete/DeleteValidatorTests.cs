namespace Taskify.Web.Endpoints.Tasks.ToDoItems.Delete;

using FluentValidation.TestHelper;

using Moq;

using System.Threading;
using System.Threading.Tasks;

using Taskify.Tasks.Core.ToDoItemAggregate;
using Taskify.Tasks.UseCases.ToDoItems.Delete;
using Taskify.SharedKernel.Data;

using Xunit;

public class DeleteValidatorTests
{
    private readonly Mock<IRepository<ToDoItem>> _repositoryMock;
    private readonly DeleteValidator _validator;

    public DeleteValidatorTests()
    {
        _repositoryMock = new Mock<IRepository<ToDoItem>>();
        _validator = new DeleteValidator(_repositoryMock.Object);
    }

    [Fact]
    public async Task ShouldHaveErrorWhenEntityDoesNotExist()
    {
        // Arrange
        var command = new DeleteToDoItemCommand(1);
        _repositoryMock.Setup(x => x.GetByIdAsync(command.Id, CancellationToken.None))
            .ReturnsAsync(() => null);

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
        var command = new DeleteToDoItemCommand(1);
        _repositoryMock.Setup(x => x.GetByIdAsync(command.Id, CancellationToken.None))
            .ReturnsAsync(new ToDoItem());

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x);
    }
}
