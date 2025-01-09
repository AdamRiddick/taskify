namespace Taskify.Web.Endpoints.Identity.ContextTypes;

using FastEndpoints;

using MediatR;

using Taskify.Identity.UseCases.ContextTypes.Create;
using Taskify.SharedKernel.Security;
using Taskify.Web.Authorization;
using Taskify.Web.Authorization.Attributes;

[ScopeAuthorization(Scopes.Identity.ContextTypes.All, Scopes.Identity.ContextTypes.Write)]
public sealed class CreateEndpoint : Endpoint<CreateContextTypeDto, int>
{
    private readonly IMediator _mediator;

    public CreateEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Post("api/identity/contexttypes");
        Policies(PolicyNames.HasScope);
    }

    public override async Task HandleAsync(
        CreateContextTypeDto request,
        CancellationToken ct)
    {
        var response = await _mediator.Send(new CreateContextTypeCommand(request), ct);
        await SendAsync(response);
    }
}