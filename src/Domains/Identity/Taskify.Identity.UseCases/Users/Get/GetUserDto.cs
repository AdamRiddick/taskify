namespace Taskify.Identity.UseCases.Users.Get;

public class GetUserDto
{
    public int Id { get; set; }

    public string EmailAddress { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;
}
