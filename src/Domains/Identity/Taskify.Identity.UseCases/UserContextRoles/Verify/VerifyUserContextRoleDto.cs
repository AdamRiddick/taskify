namespace Taskify.Identity.UseCases.UserContextRoles.Verify;

using Taskify.SharedKernel.Security;

public class VerifyUserContextRoleDto
{
    public int? ContextId { get; set; }

    public string ContextType { get; set; } = string.Empty;

    public Role Role { get; set; }

    public int UserId { get; set; }
}
