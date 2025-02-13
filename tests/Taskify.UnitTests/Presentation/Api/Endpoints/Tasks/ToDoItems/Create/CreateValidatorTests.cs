namespace Taskify.UnitTests.Presentation.Api.Endpoints.Tasks.ToDoItems.Create;

using FluentValidation.TestHelper;

using Taskify.Tasks.UseCases.ToDoItems.Create;
using Taskify.Api.Endpoints.Tasks.ToDoItems.Create;

using Xunit;
using Moq;
using Taskify.SharedKernel.Data;
using Taskify.Identity.Core.UserAggregate;

public class CreateValidatorTests
{
    private readonly CreateValidator _validator;
    private readonly Mock<IReadRepository<User>> _userRepository;

    public CreateValidatorTests()
    {
        _userRepository = new Mock<IReadRepository<User>>();
        _validator = new CreateValidator(_userRepository.Object);
    }

    [Fact]
    public async Task ShouldHaveErrorWhenTitleIsNull()
    {
        var command = new CreateToDoItemCommand(new CreateToDoItemDto());

        var result = await _validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(x => x.Dto.Title);
    }

    [Fact]
    public async Task ShouldHaveErrorWhenTitleExceedsMaximumLength()
    {
        var command = new CreateToDoItemCommand(new CreateToDoItemDto
        {
            Title = "a".PadRight(256, 'a')
        });

        var result = await _validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(x => x.Dto.Title);
    }

    [Fact]
    public async Task ShouldHaveErrorWhenDescriptionExceedsMaximumLength()
    {
        var command = new CreateToDoItemCommand(new CreateToDoItemDto
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
        var command = new CreateToDoItemCommand(new CreateToDoItemDto
        {
            Title = "My Title",
            AssigneeId = 1
        });

        _userRepository.Setup(x => x.GetByIdAsync(1, default)).ReturnsAsync(() => null);

        var result = await _validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(x => x.Dto.AssigneeId);
    }

    [Fact]
    public async Task ShouldNotHaveErrorWhenValidCommandIsProvided()
    {
        var command = new CreateToDoItemCommand(new CreateToDoItemDto
        {
            Title = "test@example.com"
        });

        var result = await _validator.TestValidateAsync(command);

        result.ShouldNotHaveValidationErrorFor(x => x.Dto);
    }
}
