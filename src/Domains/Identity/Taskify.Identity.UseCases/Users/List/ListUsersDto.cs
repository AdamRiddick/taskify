namespace Taskify.Identity.UseCases.Users.List;

using Taskify.SharedKernel.Security;

public class ListUsersDto
{
    public int Id { get; set; }
    public int ContextTypeId { get; set; }
    public Role Role { get; set; }
    public int UserId { get; set; }
}
