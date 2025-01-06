namespace Taskify.Web.Endpoints.Identity.UserContextRoles;

using FastEndpoints;

using MediatR;

using Taskify.Identity.UseCases.UserContextRoles.Create;

public sealed class CreateEndpoint : Endpoint<CreateUserContextRoleDto, int>
{
    private readonly IMediator _mediator;

    public CreateEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        AllowAnonymous();
        Post("api/identity/usercontextroles");
    }

    public override async Task HandleAsync(
        CreateUserContextRoleDto request,
        CancellationToken ct)
    {
        var response = await _mediator.Send(new CreateUserContextRoleCommand(request), ct);
        await SendAsync(response);
    }
}