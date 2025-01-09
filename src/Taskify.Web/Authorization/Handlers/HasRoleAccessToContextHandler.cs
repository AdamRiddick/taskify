namespace Taskify.Web.Authorization.Handlers;

using MediatR;

using Microsoft.AspNetCore.Authorization;

using System.Threading.Tasks;

using Taskify.Identity.UseCases.UserContextRoles.Verify;
using Taskify.Web.Authorization.Attributes;
using Taskify.Web.Authorization.Requirements;
using Taskify.Web.Extensions;

public class HasRoleAccessToContextHandler : AuthorizationHandler<HasRoleAccessToContextRequirement>
{
    private readonly IMediator _mediator;

    public HasRoleAccessToContextHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        HasRoleAccessToContextRequirement requirement)
    {
        if (context.Resource is HttpContext httpContext)
        {
            var endpoint = httpContext.GetEndpoint();
            var attribute = endpoint?.Metadata.GetMetadata<ContextAuthorizationAttribute>();

            if (attribute != null)
            {
                var dto = new VerifyUserContextRoleDto
                {
                    ContextId = attribute.ContextId,
                    ContextType = attribute.ContextType,
                    Role = attribute.MinimumRole,
                    UserId = context.User.GetUserId()
                };

                var verification = await _mediator.Send(new VerifyUserContextRoleQuery(dto));
                if (verification)
                {
                    context.Succeed(requirement);
                    return;
                }
            }

            throw new InvalidOperationException("Ensure the ContextAuthorizationAttribute is applied.");
        }
        else
        {
            throw new InvalidOperationException("Ensure this policy is used in a web context.");
        }
    }
}
