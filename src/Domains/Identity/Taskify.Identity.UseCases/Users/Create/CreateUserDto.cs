namespace Taskify.Identity.UseCases.Users.Create;

public class CreateUserDto
{
    public string EmailAddress { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;
}
