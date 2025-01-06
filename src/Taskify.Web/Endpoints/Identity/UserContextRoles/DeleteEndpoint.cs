namespace Taskify.Web.Endpoints.Identity.UserContextRoles;

using Ardalis.Result;

using FastEndpoints;

using MediatR;

using Taskify.Identity.UseCases.UserContextRoles.Delete;

public sealed class DeleteEndpoint : Endpoint<DeleteUserContextRoleCommand, Result>
{
    private readonly IMediator _mediator;

    public DeleteEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        AllowAnonymous();
        Delete("api/identity/usercontextroles/{id}");
    }

    public override async Task HandleAsync(
        DeleteUserContextRoleCommand request,
        CancellationToken ct)
    {
        var response = await _mediator.Send(request, ct);
        await SendAsync(response);
    }
}