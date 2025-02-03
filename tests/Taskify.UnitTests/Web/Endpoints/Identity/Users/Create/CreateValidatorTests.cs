namespace Taskify.Web.Tests.Endpoints.Identity.Users.Create;

using FluentValidation.TestHelper;

using Taskify.Identity.UseCases.Users.Create;
using Taskify.SharedKernel.Notifications;
using Taskify.Web.Endpoints.Identity.Users.Create;

using Xunit;

public class CreateValidatorTests
{
    private readonly CreateValidator _validator;

    public CreateValidatorTests()
    {
        _validator = new CreateValidator();
    }

    [Fact]
    public async Task ShouldHaveErrorWhenEmailAddressIsNull()
    {
        var command = new CreateUserCommand(new CreateUserDto());

        var result = await _validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(x => x.Dto.EmailAddress);
    }

    [Fact]
    public async Task ShouldHaveErrorWhenEmailAddressIsInvalid()
    {
        var command = new CreateUserCommand(new CreateUserDto
        {
            EmailAddress = "invalidemail"
        });

        var result = await _validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(x => x.Dto.EmailAddress);
    }

    [Fact]
    public async Task ShouldHaveErrorWhenEmailAddressExceedsMaximumLength()
    {
        var command = new CreateUserCommand(new CreateUserDto
        {
            EmailAddress = "a".PadRight(255, 'a')
        });

        var result = await _validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(x => x.Dto.EmailAddress);
    }

    [Fact]
    public async Task ShouldHaveErrorWhenNameIsNull()
    {
        var command = new CreateUserCommand(new CreateUserDto());

        var result = await _validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(x => x.Dto.Name);
    }

    [Fact]
    public async Task ShouldHaveErrorWhenNameIsTooShort()
    {
        var command = new CreateUserCommand(new CreateUserDto
        {
            Name = "ab"
        });

        var result = await _validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(x => x.Dto.Name);
    }

    [Fact]
    public async Task ShouldHaveErrorWhenNameExceedsMaximumLength()
    {
        var command = new CreateUserCommand(new CreateUserDto
        {
            Name = "a".PadRight(101, 'a')
        });

        var result = await _validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(x => x.Dto.Name);
    }

    [Fact]
    public async Task ShouldHaveErrorWhenDuplicateNotificationPreferencesExist()
    {
        var command = new CreateUserCommand(new CreateUserDto
        {
            NotificationPreferences =
                [
                    new CreateNotificationPreferenceDto
                    {
                        NotificationType = NotificationType.System,
                        NotificationChannel = NotificationChannel.InApp
                    },
                    new CreateNotificationPreferenceDto
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
        var command = new CreateUserCommand(new CreateUserDto
        {
            EmailAddress = "test@example.com",
            Name = "John Doe",
            NotificationPreferences =
                [
                    new CreateNotificationPreferenceDto
                    {
                        NotificationType = NotificationType.System,
                        NotificationChannel = NotificationChannel.InApp
                    },
                    new CreateNotificationPreferenceDto
                    {
                       NotificationType = NotificationType.System,
                      NotificationChannel = NotificationChannel.InApp
                    }
                ]
        });

        var result = await _validator.TestValidateAsync(command);

        result.ShouldNotHaveValidationErrorFor(x => x.Dto);
    }
}
