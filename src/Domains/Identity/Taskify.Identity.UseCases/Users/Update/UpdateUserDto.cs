namespace Taskify.Identity.UseCases.Users.Update;

using Taskify.Identity.UseCases.Users.Create;

public class UpdateUserDto
{
    public string EmailAddress { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public IEnumerable<CreateNotificationPreferenceDto> NotificationPreferences { get; set; } = [];
}
