namespace Taskify.Web.Endpoints.Identity.Users;

using Ardalis.Result;

using FastEndpoints;

using MediatR;

using Taskify.Identity.UseCases.Users.Delete;

public sealed class DeleteEndpoint : Endpoint<DeleteUserCommand, Result>
{
    private readonly IMediator _mediator;

    public DeleteEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        AllowAnonymous();
        Delete("api/tasks/users/{id}");
    }

    public override async Task HandleAsync(
        DeleteUserCommand request,
        CancellationToken ct)
    {
        var response = await _mediator.Send(request, ct);
        await SendAsync(response);
    }
}