namespace Taskify.Api.Endpoints.Tasks.ToDoItems.List;

using Ardalis.Result;

using FastEndpoints;

using MediatR;

using Taskify.Api.Authorization;
using Taskify.SharedKernel.Security;
using Taskify.Tasks.UseCases.ToDoItems.List;
using Taskify.Api.Authorization.Attributes;

[ContextAuthorization(SecurityContexts.Tasks.ToDoItem, Role.Reader)]
[ScopeAuthorization(Scopes.Tasks.ToDoItem.All, Scopes.Tasks.ToDoItem.Read)]
public sealed class ListEndpoint : Endpoint<ListToDoItemsQuery, Result<IEnumerable<ListToDoItemDto>>>
{
    private readonly IMediator _mediator;

    public ListEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Get("api/tasks/todoitems");
        Policies(PolicyNames.HasScope, PolicyNames.HasRoleAccessToContext);
    }

    public override async Task HandleAsync(
        ListToDoItemsQuery request,
        CancellationToken ct)
    {
        var response = await _mediator.Send(request, ct);
        await SendAsync(response);
    }
}