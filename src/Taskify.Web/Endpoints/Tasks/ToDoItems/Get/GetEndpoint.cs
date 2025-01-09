namespace Taskify.Web.Endpoints.Tasks.ToDoItems.Get;

using FastEndpoints;

using MediatR;

using Taskify.SharedKernel.Security;
using Taskify.Tasks.UseCases.ToDoItems.Get;
using Taskify.Web.Authorization;
using Taskify.Web.Authorization.Attributes;

[ContextAuthorization(SecurityContexts.Tasks.ToDoItem, Role.Reader)]
[ScopeAuthorization(Scopes.Tasks.ToDoItem.All, Scopes.Tasks.ToDoItem.Read)]
public sealed class GetEndpoint : Endpoint<GetToDoItemQuery, GetToDoItemDto>
{
    private readonly IMediator _mediator;

    public GetEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Get("api/tasks/todoitem/{id}");
        Policies(PolicyNames.HasScope, PolicyNames.HasRoleAccessToContext);
    }

    public override async Task HandleAsync(
        GetToDoItemQuery request,
        CancellationToken ct)
    {
        var response = await _mediator.Send(request, ct);
        await SendAsync(response.Value);
    }
}