namespace Taskify.Web.Endpoints.Identity.ContextTypes;

using Ardalis.Result;

using FastEndpoints;

using MediatR;

using Taskify.Identity.UseCases.ContextTypes.Delete;

public sealed class DeleteEndpoint : Endpoint<DeleteContextTypeCommand, Result>
{
    private readonly IMediator _mediator;

    public DeleteEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        AllowAnonymous();
        Delete("api/identity/contexttypes/{id}");
    }

    public override async Task HandleAsync(
        DeleteContextTypeCommand request,
        CancellationToken ct)
    {
        var response = await _mediator.Send(request, ct);
        await SendAsync(response);
    }
}