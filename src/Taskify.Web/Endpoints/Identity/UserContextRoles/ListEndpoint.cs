namespace Taskify.Web.Endpoints.Identity.UserContextRoles;

using Ardalis.Result;

using FastEndpoints;

using MediatR;

using Taskify.Identity.UseCases.UserContextRoles.List;

public sealed class ListEndpoint : Endpoint<ListUserContextRolesQuery, Result<IEnumerable<ListUserContextRolesDto>>>
{
    private readonly IMediator _mediator;

    public ListEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        AllowAnonymous();
        Get("api/identity/usercontextroles");
    }

    public override async Task HandleAsync(
        ListUserContextRolesQuery request,
        CancellationToken ct)
    {
        var response = await _mediator.Send(request, ct);
        await SendAsync(response);
    }
}