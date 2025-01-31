namespace Taskify.Identity.UseCases.Users.List;

public class ListUsersDto
{
    public int Id { get; set; }

    public string EmailAddress { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;
}
