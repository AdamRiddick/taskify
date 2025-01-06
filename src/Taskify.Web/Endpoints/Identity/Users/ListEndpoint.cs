namespace Taskify.Web.Endpoints.Identity.Users;

using Ardalis.Result;

using FastEndpoints;

using MediatR;

using Taskify.Identity.UseCases.Users.List;

public sealed class ListEndpoint : Endpoint<ListUsersQuery, Result<IEnumerable<ListUsersDto>>>
{
    private readonly IMediator _mediator;

    public ListEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        AllowAnonymous();
        Get("api/tasks/users");
    }

    public override async Task HandleAsync(
        ListUsersQuery request,
        CancellationToken ct)
    {
        var response = await _mediator.Send(request, ct);
        await SendAsync(response);
    }
}