namespace Taskify.Identity.UseCases.UserContextRoles.List;

using Taskify.SharedKernel.Security;

public class ListUserContextRolesDto
{
    public int Id { get; set; }
    public int ContextTypeId { get; set; }
    public Role Role { get; set; }
    public int UserId { get; set; }
}
