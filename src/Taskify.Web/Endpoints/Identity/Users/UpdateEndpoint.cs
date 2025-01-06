namespace Taskify.Web.Endpoints.Identity.Users;

using Ardalis.Result;

using FastEndpoints;

using MediatR;

using Taskify.Identity.UseCases.Users.Update;

public sealed class UpdateEndpoint : Endpoint<UpdateUserDto, Result>
{
    private readonly IMediator _mediator;

    public UpdateEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        AllowAnonymous();
        Put("api/tasks/users/{id}");
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