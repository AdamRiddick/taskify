namespace Taskify.Web.Endpoints.Identity.Users;

using FastEndpoints;

using MediatR;

using Taskify.Identity.UseCases.Users.Create;
using Taskify.SharedKernel.Security;
using Taskify.Web.Authorization;
using Taskify.Web.Authorization.Attributes;

[ContextAuthorization(SecurityContexts.Identity.Users, Role.Contributor)]
[ScopeAuthorization(Scopes.Identity.Users.All, Scopes.Identity.Users.Write)]
public sealed class CreateEndpoint : Endpoint<CreateUserDto, int>
{
    private readonly IMediator _mediator;

    public CreateEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Post("api/identity/users");
        Policies(PolicyNames.HasScope, PolicyNames.HasRoleAccessToContext);
    }

    public override async Task HandleAsync(
        CreateUserDto request,
        CancellationToken ct)
    {
        var response = await _mediator.Send(new CreateUserCommand(request), ct);

        await SendCreatedAtAsync<GetEndpoint>(
            new { Id = response.Value },
        response);
    }
}