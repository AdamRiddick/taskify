namespace Taskify.Api.Authorization.Handlers;

using Microsoft.AspNetCore.Authorization;

using System.Threading.Tasks;

using Taskify.Api.Authorization.Attributes;
using Taskify.Api.Authorization.Requirements;
using Taskify.Api.Extensions;

public class HasScopeHandler : AuthorizationHandler<HasScopeRequirement>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        HasScopeRequirement requirement)
    {
        if (context.Resource is HttpContext httpContext)
        {
            var endpoint = httpContext.GetEndpoint();
            var attribute = (endpoint?.Metadata.GetMetadata<ScopeAuthorizationAttribute>()) ?? throw new InvalidOperationException("Ensure the ScopeAuthorizationAttribute is applied.");

            if (attribute.AllowedScopes.Any(x => context.User.HasScope(x)))
            {
                context.Succeed(requirement);
            }

            context.Fail();
            return Task.CompletedTask;
        }
         
        throw new InvalidOperationException("Ensure this policy is used in a web context.");
    }
}
