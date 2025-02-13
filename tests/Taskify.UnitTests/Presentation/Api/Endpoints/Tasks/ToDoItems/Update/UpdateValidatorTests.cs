namespace Taskify.UnitTests.Presentation.Api.Endpoints.Tasks.ToDoItems.Update;

using FluentValidation.TestHelper;

using Moq;

using Taskify.Tasks.Core.ToDoItemAggregate;
using Taskify.Tasks.UseCases.ToDoItems.Update;
using Taskify.SharedKernel.Data;
using Taskify.Api.Endpoints.Tasks.ToDoItems.Update;

using Xunit;

using Taskify.Identity.Core.UserAggregate;

public class UpdateValidatorTests
{
    private readonly Mock<IReadRepository<ToDoItem>> _repositoryMock;
    private readonly Mock<IReadRepository<User>> _userRepositoryMock;
    private readonly UpdateValidator _validator;

    public UpdateValidatorTests()
    {
        _repositoryMock = new Mock<IReadRepository<ToDoItem>>();
        _userRepositoryMock = new Mock<IReadRepository<User>>();
        _validator = new UpdateValidator(_repositoryMock.Object, _userRepositoryMock.Object);
    }

    [Fact]
    public async Task ShouldHaveErrorWhenTitleIsNull()
    {
        var command = new UpdateToDoItemCommand(1, new UpdateToDoItemDto());

        var result = await _validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(x => x.Dto.Title);
    }

    [Fact]
    public async Task ShouldHaveErrorWhenTitleExceedsMaximumLength()
    {
        var command = new UpdateToDoItemCommand(1, new UpdateToDoItemDto
        {
            Title = "a".PadRight(256, 'a')
        });

        var result = await _validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(x => x.Dto.Title);
    }

    [Fact]
    public async Task ShouldHaveErrorWhenDescriptionExceedsMaximumLength()
    {
        var command = new UpdateToDoItemCommand(1, new UpdateToDoItemDto
        {
            Title = "My Title",
            Description = "a".PadRight(4001, 'a')
        });

        var result = await _validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(x => x.Dto.Description);
    }

    [Fact]
    public async Task ShouldHaveErrorWhenUserDoesNotExist()
    {
        var command = new UpdateToDoItemCommand(1, new UpdateToDoItemDto
        {
            Title = "My Title",
            AssigneeId = 1
        });

        _userRepositoryMock.Setup(x => x.GetByIdAsync(1, default)).ReturnsAsync(() => null);

        var result = await _validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(x => x.Dto.AssigneeId);
    }

    [Fact]
    public async Task ShouldNotHaveErrorWhenValidCommandIsProvided()
    {
        var command = new UpdateToDoItemCommand(1, new UpdateToDoItemDto
        {
            Title = "test@example.com"
        });

        var result = await _validator.TestValidateAsync(command);

        result.ShouldNotHaveValidationErrorFor(x => x.Dto);
    }

    [Fact]
    public async Task ShouldHaveErrorWhenEntityDoesNotExist()
    {
        // Arrange
        var command = new UpdateToDoItemCommand(1, new UpdateToDoItemDto
        {
            Title = "My Title"
        });

        _repositoryMock.Setup(x => x.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => null);

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x)
            .WithErrorMessage("Entity does not exist.");
    }
}
