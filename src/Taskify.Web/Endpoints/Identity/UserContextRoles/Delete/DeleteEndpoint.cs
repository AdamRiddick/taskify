namespace Taskify.Web.Endpoints.Identity.UserContextRoles.Delete;

using Ardalis.Result;

using FastEndpoints;

using MediatR;

using Taskify.Identity.UseCases.UserContextRoles.Delete;
using Taskify.SharedKernel.Security;
using Taskify.Web.Authorization;
using Taskify.Web.Authorization.Attributes;

[ContextAuthorization(SecurityContexts.Identity.UserContextRoles, Role.Contributor)]
[ScopeAuthorization(Scopes.Identity.UserContextRoles.All, Scopes.Identity.UserContextRoles.Write)]
public sealed class DeleteEndpoint : Endpoint<DeleteUserContextRoleCommand, Result>
{
    private readonly IMediator _mediator;

    public DeleteEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Delete("api/identity/usercontextroles/{id}");
        Policies(PolicyNames.HasScope, PolicyNames.HasRoleAccessToContext);
    }

    public override async Task HandleAsync(
        DeleteUserContextRoleCommand request,
        CancellationToken ct)
    {
        var response = await _mediator.Send(request, ct);
        await SendAsync(response);
    }
}