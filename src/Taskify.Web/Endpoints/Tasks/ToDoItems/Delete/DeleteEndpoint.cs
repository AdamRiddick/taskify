namespace Taskify.Web.Endpoints.Tasks.ToDoItems.Delete;

using Ardalis.Result;

using FastEndpoints;

using MediatR;

using Taskify.SharedKernel.Security;
using Taskify.Tasks.UseCases.ToDoItems.Delete;
using Taskify.Web.Authorization;
using Taskify.Web.Authorization.Attributes;

[ContextAuthorization(SecurityContexts.Tasks.ToDoItem, Role.Contributor)]
[ScopeAuthorization(Scopes.Tasks.ToDoItem.All, Scopes.Tasks.ToDoItem.Write)]
public sealed class DeleteEndpoint : Endpoint<DeleteToDoItemCommand, Result>
{
    private readonly IMediator _mediator;

    public DeleteEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Delete("api/tasks/todoitems/{id}");
        Policies(PolicyNames.HasScope, PolicyNames.HasRoleAccessToContext);
    }

    public override async Task HandleAsync(
        DeleteToDoItemCommand request,
        CancellationToken ct)
    {
        var response = await _mediator.Send(request, ct);
        await SendAsync(response);
    }
}