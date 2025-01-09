namespace Taskify.Web.Endpoints.Identity.ContextTypes.List;

using Ardalis.Result;

using FastEndpoints;

using MediatR;

using Taskify.Identity.UseCases.ContextTypes.List;
using Taskify.SharedKernel.Security;
using Taskify.Web.Authorization;
using Taskify.Web.Authorization.Attributes;

[ScopeAuthorization(Scopes.Identity.ContextTypes.All, Scopes.Identity.ContextTypes.Read)]
public sealed class ListEndpoint : Endpoint<ListContextTypesQuery, Result<IEnumerable<ListContextTypeDto>>>
{
    private readonly IMediator _mediator;

    public ListEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Get("api/identity/contexttypes");
        Policies(PolicyNames.HasScope);
    }

    public override async Task HandleAsync(
        ListContextTypesQuery request,
        CancellationToken ct)
    {
        var response = await _mediator.Send(request, ct);
        await SendAsync(response);
    }
}