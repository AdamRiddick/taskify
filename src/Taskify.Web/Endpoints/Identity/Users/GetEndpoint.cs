namespace Taskify.Web.Endpoints.Identity.Users;

using FastEndpoints;

using MediatR;

using Taskify.Identity.UseCases.Users.Get;

public sealed class GetEndpoint : Endpoint<GetUserQuery, GetUserDto>
{
    private readonly IMediator _mediator;

    public GetEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        AllowAnonymous();
        Get("api/tasks/user/{id}");
    }

    public override async Task HandleAsync(
        GetUserQuery request,
        CancellationToken ct)
    {
        var response = await _mediator.Send(request, ct);
        await SendAsync(response.Value);
    }
}