namespace Taskify.Web.Endpoints.Tasks.ToDoItems.MarkToDoItemComplete;

using Ardalis.Result;

using FastEndpoints;

using MediatR;

using Taskify.SharedKernel.Security;
using Taskify.Tasks.UseCases.ToDoItems.MarkToDoItemComplete;
using Taskify.Web.Authorization;
using Taskify.Web.Authorization.Attributes;

[ContextAuthorization(SecurityContexts.Tasks.ToDoItem, Role.Contributor)]
[ScopeAuthorization(Scopes.Tasks.ToDoItem.All, Scopes.Tasks.ToDoItem.Write)]
public sealed class MarkCompleteEndpoint : Endpoint<MarkToDoItemCompleteCommand, Result>
{
    private readonly IMediator _mediator;

    public MarkCompleteEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Post("api/tasks/todoitems/{id}/complete");
        Policies(PolicyNames.HasScope, PolicyNames.HasRoleAccessToContext);

        // see: https://github.com/FastEndpoints/FastEndpoints/issues/492#issuecomment-1740210893
        Description(x => x.Accepts<MarkToDoItemCompleteCommand>());
    }

    public override async Task HandleAsync(
        MarkToDoItemCompleteCommand request,
        CancellationToken ct)
    {
        var response = await _mediator.Send(request, ct);
        await SendAsync(response);
    }
}