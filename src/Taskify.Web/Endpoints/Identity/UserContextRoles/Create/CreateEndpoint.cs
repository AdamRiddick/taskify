namespace Taskify.Web.Endpoints.Identity.UserContextRoles.Create;

using FastEndpoints;

using MediatR;

using Taskify.Identity.UseCases.UserContextRoles.Create;
using Taskify.SharedKernel.Security;
using Taskify.Web.Authorization;
using Taskify.Web.Authorization.Attributes;

[ContextAuthorization(SecurityContexts.Identity.UserContextRoles, Role.Contributor)]
[ScopeAuthorization(Scopes.Identity.UserContextRoles.All, Scopes.Identity.UserContextRoles.Write)]
public sealed class CreateEndpoint : Endpoint<CreateUserContextRoleDto, int>
{
    private readonly IMediator _mediator;

    public CreateEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Post("api/identity/usercontextroles");
        Policies(PolicyNames.HasScope, PolicyNames.HasRoleAccessToContext);
    }

    public override async Task HandleAsync(
        CreateUserContextRoleDto request,
        CancellationToken ct)
    {
        var response = await _mediator.Send(new CreateUserContextRoleCommand(request), ct);
        await SendAsync(response);
    }
}