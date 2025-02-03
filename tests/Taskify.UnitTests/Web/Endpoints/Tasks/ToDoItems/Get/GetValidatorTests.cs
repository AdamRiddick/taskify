namespace Taskify.Web.Endpoints.Tasks.ToDoItems.Get;

using FluentValidation.TestHelper;

using Moq;

using System.Threading;
using System.Threading.Tasks;

using Taskify.Tasks.Core.ToDoItemAggregate;
using Taskify.Tasks.UseCases.ToDoItems.Get;
using Taskify.SharedKernel.Data;

using Xunit;
public class GetValidatorTests
{
    private readonly Mock<IReadRepository<ToDoItem>> _repositoryMock;
    private readonly GetValidator _validator;

    public GetValidatorTests()
    {
        _repositoryMock = new Mock<IReadRepository<ToDoItem>>();
        _validator = new GetValidator(_repositoryMock.Object);
    }

    [Fact]
    public async Task ShouldHaveErrorWhenEntityDoesNotExist()
    {
        // Arrange
        var query = new GetToDoItemQuery(1);
        _repositoryMock.Setup(x => x.GetByIdAsync(query.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(()=> null);

        // Act
        var result = await _validator.TestValidateAsync(query);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x)
            .WithErrorMessage("Entity does not exist.");
    }

    [Fact]
    public async Task ShouldNotHaveErrorWhenEntityExists()
    {
        // Arrange
        var query = new GetToDoItemQuery(1);
        _repositoryMock.Setup(x => x.GetByIdAsync(query.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ToDoItem());

        // Act
        var result = await _validator.TestValidateAsync(query);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x);
    }
}
