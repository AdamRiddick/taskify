namespace Taskify.Web.Endpoints.Tasks.ToDoItems.Update;

using Ardalis.Result;

using FastEndpoints;

using MediatR;

using Taskify.SharedKernel.Security;
using Taskify.Tasks.UseCases.ToDoItems.Update;
using Taskify.Web.Authorization;
using Taskify.Web.Authorization.Attributes;

[ContextAuthorization(SecurityContexts.Tasks.ToDoItem, Role.Contributor)]
[ScopeAuthorization(Scopes.Tasks.ToDoItem.All, Scopes.Tasks.ToDoItem.Write)]
public sealed class UpdateEndpoint : Endpoint<UpdateToDoItemDto, Result>
{
    private readonly IMediator _mediator;

    public UpdateEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Put("api/tasks/todoitems/{id}");
        Policies(PolicyNames.HasScope, PolicyNames.HasRoleAccessToContext);
    }

    public override async Task HandleAsync(
        UpdateToDoItemDto request,
        CancellationToken ct)
    {
        var id = Route<int>("id");
        var response = await _mediator.Send(new UpdateToDoItemCommand(id, request), ct);
        await SendAsync(response);
    }
}