namespace Taskify.Web.Authorization.Attributes;

using Microsoft.AspNetCore.Authorization;

using Taskify.SharedKernel.Security;

[AttributeUsage(AttributeTargets.Class)]
public class ContextAuthorizationAttribute : AuthorizeAttribute
{
    public int? ContextId { get; }
    public string ContextType { get; }
    public Role MinimumRole { get; }

    public ContextAuthorizationAttribute(string contextType)
        : this(contextType, Role.Reader)
    {
    }

    public ContextAuthorizationAttribute(
        string contextType, 
        Role minimumRole)
    {
        ContextType = contextType;
        MinimumRole = minimumRole;
        Policy = "HasRoleAccessToContext";
    }

    public ContextAuthorizationAttribute(
        string contextType,
        Role minimumRole,
        int contextId)
    {
        ContextId = contextId;
        ContextType = contextType;
        MinimumRole = minimumRole;
        Policy = "HasRoleAccessToContext";
    }
}
