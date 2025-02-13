namespace Taskify.Api.Authorization.Attributes;

using Microsoft.AspNetCore.Authorization;

[AttributeUsage(AttributeTargets.Class)]
public class ScopeAuthorizationAttribute : AuthorizeAttribute
{
    public string[] AllowedScopes { get; }

    public ScopeAuthorizationAttribute(params string[] allowedScopes)
    {
        AllowedScopes = allowedScopes;
    }
}
