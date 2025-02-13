namespace Taskify.UnitTests.Presentation.Api.Endpoints.Identity.ContextTypes.Create;

using FluentValidation.TestHelper;

using Moq;

using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

using Taskify.Identity.Core.ContextTypeAggregate;
using Taskify.Identity.UseCases.ContextTypes.Create;
using Taskify.SharedKernel.Data;
using Taskify.Api.Endpoints.Identity.ContextTypes.Create;

using Xunit;

public class CreateValidatorTests
{
    private readonly Mock<IRepository<ContextType>> _repositoryMock;
    private readonly CreateValidator _validator;

    public CreateValidatorTests()
    {
        _repositoryMock = new Mock<IRepository<ContextType>>();
        _validator = new CreateValidator(_repositoryMock.Object);
    }

    [Fact]
    public async Task ShouldHaveValidationErrorWhenDtoNameIsEmpty()
    {
        // Arrange
        var command = new CreateContextTypeCommand(new CreateContextTypeDto { Name = "" });

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Dto.Name);
    }

    [Fact]
    public async Task ShouldHaveValidationErrorWhenDtoNameExceedsMaximumLength()
    {
        // Arrange
        var command = new CreateContextTypeCommand(new CreateContextTypeDto { Name = new string('A', 256) });

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Dto.Name);
    }

    [Fact]
    public async Task ShouldHaveValidationErrorWhenDtoNameAlreadyExists()
    {
        // Arrange
        var existingContextType = new ContextType { Name = "ExistingContextType" };
        _repositoryMock.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ContextType, bool>>>(), CancellationToken.None))
            .ReturnsAsync(existingContextType);

        var command = new CreateContextTypeCommand(new CreateContextTypeDto { Name = "ExistingContextType" });

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Dto)
            .WithErrorMessage("Context type with the same name already exists.");
    }

    [Fact]
    public async Task ShouldNotHaveValidationErrorWhenDtoIsValid()
    {
        // Arrange
        _repositoryMock.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ContextType, bool>>>(), CancellationToken.None))
            .ReturnsAsync(() => null);

        var command = new CreateContextTypeCommand(new CreateContextTypeDto { Name = "NewContextType" });

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
}
