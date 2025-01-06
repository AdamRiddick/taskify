namespace Taskify.Identity.UseCases.Users.Get;

public class GetUserDto
{
    public string EmailAddress { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;
}
