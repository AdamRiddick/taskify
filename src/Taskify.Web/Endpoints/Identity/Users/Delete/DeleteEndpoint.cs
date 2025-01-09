namespace Taskify.Web.Endpoints.Identity.Users.Delete;

using Ardalis.Result;

using FastEndpoints;

using MediatR;

using Taskify.Identity.UseCases.Users.Delete;
using Taskify.SharedKernel.Security;
using Taskify.Web.Authorization;
using Taskify.Web.Authorization.Attributes;

[ContextAuthorization(SecurityContexts.Identity.Users, Role.Contributor)]
[ScopeAuthorization(Scopes.Identity.Users.All, Scopes.Identity.Users.Write)]
public sealed class DeleteEndpoint : Endpoint<DeleteUserCommand, Result>
{
    private readonly IMediator _mediator;

    public DeleteEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Delete("api/tasks/users/{id}");
        Policies(PolicyNames.HasScope, PolicyNames.HasRoleAccessToContext);
    }

    public override async Task HandleAsync(
        DeleteUserCommand request,
        CancellationToken ct)
    {
        var response = await _mediator.Send(request, ct);
        await SendAsync(response);
    }
}