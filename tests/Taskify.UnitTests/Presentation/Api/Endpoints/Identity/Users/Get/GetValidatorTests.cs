namespace Taskify.UnitTests.Presentation.Api.Endpoints.Identity.Users.Get;

using FluentValidation.TestHelper;

using Moq;

using System.Threading;
using System.Threading.Tasks;

using Taskify.Api.Endpoints.Identity.Users.Get;
using Taskify.Identity.Core.UserAggregate;
using Taskify.Identity.UseCases.Users.Get;
using Taskify.SharedKernel.Data;

using Xunit;
public class GetValidatorTests
{
    private readonly Mock<IReadRepository<User>> _repositoryMock;
    private readonly GetValidator _validator;

    public GetValidatorTests()
    {
        _repositoryMock = new Mock<IReadRepository<User>>();
        _validator = new GetValidator(_repositoryMock.Object);
    }

    [Fact]
    public async Task ShouldHaveErrorWhenEntityDoesNotExist()
    {
        // Arrange
        var query = new GetUserQuery(1);
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
        var query = new GetUserQuery(1);
        _repositoryMock.Setup(x => x.GetByIdAsync(query.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new User());

        // Act
        var result = await _validator.TestValidateAsync(query);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x);
    }
}
