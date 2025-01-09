namespace Taskify.Web.Endpoints.Identity.Users;

using Ardalis.Result;

using FastEndpoints;

using MediatR;

using Taskify.Identity.UseCases.Users.Update;
using Taskify.SharedKernel.Security;
using Taskify.Web.Authorization;
using Taskify.Web.Authorization.Attributes;

[ContextAuthorization(SecurityContexts.Identity.Users, Role.Reader)]
[ScopeAuthorization(Scopes.Identity.Users.All, Scopes.Identity.Users.Write)]
public sealed class UpdateEndpoint : Endpoint<UpdateUserDto, Result>
{
    private readonly IMediator _mediator;

    public UpdateEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Put("api/tasks/users/{id}");
        Policies(PolicyNames.HasScope, PolicyNames.HasRoleAccessToContext);
    }

    public override async Task HandleAsync(
        UpdateUserDto request,
        CancellationToken ct)
    {
        var id = Route<int>("id");
        var response = await _mediator.Send(new UpdateUserCommand(id, request), ct);
        await SendAsync(response);
    }
}