namespace Taskify.Web.Endpoints.Identity.UserContextRoles.List;

using Ardalis.Result;

using FastEndpoints;

using MediatR;

using Taskify.Identity.UseCases.UserContextRoles.List;
using Taskify.SharedKernel.Security;
using Taskify.Web.Authorization;
using Taskify.Web.Authorization.Attributes;

[ContextAuthorization(SecurityContexts.Identity.UserContextRoles, Role.Reader)]
[ScopeAuthorization(Scopes.Identity.UserContextRoles.All, Scopes.Identity.UserContextRoles.Read)]
public sealed class ListEndpoint : Endpoint<ListUserContextRolesQuery, Result<IEnumerable<ListUserContextRolesDto>>>
{
    private readonly IMediator _mediator;

    public ListEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Get("api/identity/usercontextroles");
        Policies(PolicyNames.HasScope, PolicyNames.HasRoleAccessToContext);
    }

    public override async Task HandleAsync(
        ListUserContextRolesQuery request,
        CancellationToken ct)
    {
        var response = await _mediator.Send(request, ct);
        await SendAsync(response);
    }
}