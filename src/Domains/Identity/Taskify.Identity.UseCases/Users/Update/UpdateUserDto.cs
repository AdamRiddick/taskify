namespace Taskify.Identity.UseCases.Users.Update;

public class UpdateUserDto
{
    public string EmailAddress { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
}
