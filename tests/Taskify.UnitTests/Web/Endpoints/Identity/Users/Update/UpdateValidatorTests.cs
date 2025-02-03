namespace Taskify.Web.Tests.Endpoints.Identity.Users.Update;

using FluentValidation.TestHelper;

using Moq;

using Taskify.Identity.Core.UserAggregate;
using Taskify.Identity.UseCases.Users.Update;
using Taskify.SharedKernel.Data;
using Taskify.SharedKernel.Notifications;
using Taskify.Web.Endpoints.Identity.Users.Update;

using Xunit;

public class UpdateValidatorTests
{
    private readonly Mock<IReadRepository<User>> _repositoryMock;
    private readonly UpdateValidator _validator;

    public UpdateValidatorTests()
    {
        _repositoryMock = new Mock<IReadRepository<User>>();
        _validator = new UpdateValidator(_repositoryMock.Object);
    }

    [Fact]
    public async Task ShouldHaveErrorWhenEmailAddressIsNull()
    {
        var command = new UpdateUserCommand(1, new UpdateUserDto());

        var result = await _validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(x => x.Dto.EmailAddress);
    }

    [Fact]
    public async Task ShouldHaveErrorWhenEmailAddressIsInvalid()
    {
        var command = new UpdateUserCommand(1, new UpdateUserDto
        {
            EmailAddress = "invalidemail"
        });

        var result = await _validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(x => x.Dto.EmailAddress);
    }

    [Fact]
    public async Task ShouldHaveErrorWhenEmailAddressExceedsMaximumLength()
    {
        var command = new UpdateUserCommand(1, new UpdateUserDto
        {
            EmailAddress = "a".PadRight(255, 'a')
        });

        var result = await _validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(x => x.Dto.EmailAddress);
    }

    [Fact]
    public async Task ShouldHaveErrorWhenNameIsNull()
    {
        var command = new UpdateUserCommand(1, new UpdateUserDto());

        var result = await _validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(x => x.Dto.Name);
    }

    [Fact]
    public async Task ShouldHaveErrorWhenNameIsTooShort()
    {
        var command = new UpdateUserCommand(1, new UpdateUserDto
        {
            Name = "ab"
        });

        var result = await _validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(x => x.Dto.Name);
    }

    [Fact]
    public async Task ShouldHaveErrorWhenNameExceedsMaximumLength()
    {
        var command = new UpdateUserCommand(1, new UpdateUserDto
        {
            Name = "a".PadRight(101, 'a')
        });

        var result = await _validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(x => x.Dto.Name);
    }

    [Fact]
    public async Task ShouldHaveErrorWhenDuplicateNotificationPreferencesExist()
    {
        var command = new UpdateUserCommand(1, new UpdateUserDto
        {
            NotificationPreferences =
                [
                    new UpdateNotificationPreferenceDto
                    {
                        NotificationType = NotificationType.System,
                        NotificationChannel = NotificationChannel.InApp
                    },
                    new UpdateNotificationPreferenceDto
                    {
                       NotificationType = NotificationType.System,
                      NotificationChannel = NotificationChannel.InApp
                    }
                ]
        });

        var result = await _validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(x => x.Dto.NotificationPreferences);
    }

    [Fact]
    public async Task ShouldNotHaveErrorWhenValidCommandIsProvided()
    {
        var command = new UpdateUserCommand(1, new UpdateUserDto
        {
            EmailAddress = "test@example.com",
            Name = "John Doe",
            NotificationPreferences =
                [
                    new UpdateNotificationPreferenceDto
                    {
                        NotificationType = NotificationType.System,
                        NotificationChannel = NotificationChannel.InApp
                    },
                    new UpdateNotificationPreferenceDto
                    {
                       NotificationType = NotificationType.System,
                      NotificationChannel = NotificationChannel.InApp
                    }
                ]
        });

        var result = await _validator.TestValidateAsync(command);

        result.ShouldNotHaveValidationErrorFor(x => x.Dto);
    }

    [Fact]
    public async Task ShouldHaveErrorWhenEntityDoesNotExist()
    {
        // Arrange
        var command = new UpdateUserCommand(1, new UpdateUserDto
        {
            EmailAddress = "test@example.com",
            Name = "John Doe",
            NotificationPreferences =
        [
            new UpdateNotificationPreferenceDto
                    {
                        NotificationType = NotificationType.System,
                        NotificationChannel = NotificationChannel.InApp
                    },
                    new UpdateNotificationPreferenceDto
                    {
                       NotificationType = NotificationType.System,
                      NotificationChannel = NotificationChannel.InApp
                    }
        ]
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
