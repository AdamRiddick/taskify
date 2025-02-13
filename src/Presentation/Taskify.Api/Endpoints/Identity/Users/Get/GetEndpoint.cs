namespace Taskify.Api.Endpoints.Identity.Users.Get;

using FastEndpoints;

using MediatR;

using Taskify.Api.Authorization;
using Taskify.Identity.UseCases.Users.Get;
using Taskify.SharedKernel.Security;
using Taskify.Api.Authorization.Attributes;

[ContextAuthorization(SecurityContexts.Identity.Users, Role.Reader)]
[ScopeAuthorization(Scopes.Identity.Users.All, Scopes.Identity.Users.Read)]
public sealed class GetEndpoint : Endpoint<GetUserQuery, GetUserDto>
{
    private readonly IMediator _mediator;

    public GetEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Get("api/tasks/user/{id}");
        Policies(PolicyNames.HasScope, PolicyNames.HasRoleAccessToContext);
    }

    public override async Task HandleAsync(
        GetUserQuery request,
        CancellationToken ct)
    {
        var response = await _mediator.Send(request, ct);
        await SendAsync(response.Value);
    }
}