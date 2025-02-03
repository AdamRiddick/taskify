namespace Taskify.UnitTests.Future_Packages.Taskify.Infrastructure.Notifications.Dispatcher;

using FluentValidation.TestHelper;

using global::Taskify.Infrastructure.Notifications.Dispatcher;
using global::Taskify.SharedKernel.Notifications;

using Xunit;

public class SendNotificationValidatorTests
{
    private readonly SendNotificationValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_Message_Is_Empty()
    {
        var dto = new Notification { Message = "", Title = "Valid Title" };
        var command = new SendNotificationCommand(dto);

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.Dto.Message)
              .WithErrorMessage("'Message' must not be empty.");
    }

    [Fact]
    public void Should_Have_Error_When_Message_Exceeds_Max_Length()
    {
        var dto = new Notification {
            Message = new string('A', 1001),
            Title = "Valid Title"
        };
        var command = new SendNotificationCommand(dto);

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.Dto.Message)
              .WithErrorMessage("The length of 'Message' must be 1000 characters or fewer.");
    }

    [Fact]
    public void Should_Have_Error_When_Title_Is_Empty()
    {

        var dto = new Notification { Message = "Valid Message", Title = "" };
        var command = new SendNotificationCommand(dto);

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.Dto.Title)
              .WithErrorMessage("'Title' must not be empty.");
    }

    [Fact]
    public void Should_Have_Error_When_Title_Exceeds_Max_Length()
    {
        var dto = new Notification { 
            Message = new string('a', 101),
            Title = new string('a', 101) };
        var command = new SendNotificationCommand(dto);

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.Dto.Title)
              .WithErrorMessage("The length of 'Title' must be 100 characters or fewer.");
    }

    [Fact]
    public void Should_Pass_Validation_For_Valid_Command()
    {
        var dto = new Notification { Message = "Valid Message", Title = "Valid Title" };
        var command = new SendNotificationCommand(dto);

        var result = _validator.TestValidate(command);

        result.ShouldNotHaveAnyValidationErrors();
    }
}
