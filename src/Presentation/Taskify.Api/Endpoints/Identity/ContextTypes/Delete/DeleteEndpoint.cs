namespace Taskify.Api.Endpoints.Identity.ContextTypes.Delete;

using Ardalis.Result;

using FastEndpoints;

using MediatR;

using Taskify.Api.Authorization;
using Taskify.Identity.UseCases.ContextTypes.Delete;
using Taskify.SharedKernel.Security;
using Taskify.Api.Authorization.Attributes;

[ScopeAuthorization(Scopes.Identity.ContextTypes.All, Scopes.Identity.ContextTypes.Write)]
public sealed class DeleteEndpoint : Endpoint<DeleteContextTypeCommand, Result>
{
    private readonly IMediator _mediator;

    public DeleteEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Delete("api/identity/contexttypes/{id}");
        Policies(PolicyNames.HasScope);
    }

    public override async Task HandleAsync(
        DeleteContextTypeCommand request,
        CancellationToken ct)
    {
        var response = await _mediator.Send(request, ct);
        await SendAsync(response);
    }
}