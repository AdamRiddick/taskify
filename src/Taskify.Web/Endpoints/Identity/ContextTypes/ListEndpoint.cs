namespace Taskify.Web.Endpoints.Identity.ContextTypes;

using Ardalis.Result;

using FastEndpoints;

using MediatR;

using Taskify.Identity.UseCases.ContextTypes.List;

public sealed class ListEndpoint : Endpoint<ListContextTypesQuery, Result<IEnumerable<ListContextTypeDto>>>
{
    private readonly IMediator _mediator;

    public ListEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        AllowAnonymous();
        Get("api/identity/contexttypes");
    }

    public override async Task HandleAsync(
        ListContextTypesQuery request,
        CancellationToken ct)
    {
        var response = await _mediator.Send(request, ct);
        await SendAsync(response);
    }
}