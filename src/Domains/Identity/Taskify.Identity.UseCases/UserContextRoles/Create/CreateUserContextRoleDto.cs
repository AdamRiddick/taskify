namespace Taskify.Identity.UseCases.UserContextRoles.Create;

using Taskify.SharedKernel.Security;

public class CreateUserContextRoleDto
{
    public int ContextTypeId { get; set; }

    public Role Role { get; set; }

    public int UserId { get; set; }
}
