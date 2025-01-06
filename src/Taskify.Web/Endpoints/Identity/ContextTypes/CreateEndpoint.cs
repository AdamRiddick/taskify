namespace Taskify.Web.Endpoints.Identity.ContextTypes;

using FastEndpoints;

using MediatR;

using Taskify.Identity.UseCases.ContextTypes.Create;

public sealed class CreateEndpoint : Endpoint<CreateContextTypeDto, int>
{
    private readonly IMediator _mediator;

    public CreateEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        AllowAnonymous();
        Post("api/identity/contexttypes");
    }

    public override async Task HandleAsync(
        CreateContextTypeDto request,
        CancellationToken ct)
    {
        var response = await _mediator.Send(new CreateContextTypeCommand(request), ct);
        await SendAsync(response);
    }
}