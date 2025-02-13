namespace Taskify.Api.Endpoints.Tasks.ToDoItems.Create;

using FastEndpoints;

using MediatR;

using Taskify.Api.Authorization;
using Taskify.Api.Endpoints.Tasks.ToDoItems.Get;
using Taskify.SharedKernel.Security;
using Taskify.Tasks.UseCases.ToDoItems.Create;
using Taskify.Api.Authorization.Attributes;

[ContextAuthorization(SecurityContexts.Tasks.ToDoItem, Role.Contributor)]
[ScopeAuthorization(Scopes.Tasks.ToDoItem.All, Scopes.Tasks.ToDoItem.Write)]
public sealed class CreateEndpoint : Endpoint<CreateToDoItemDto, int>
{
    private readonly IMediator _mediator;

    public CreateEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Post("api/tasks/todoitems");
        Policies(PolicyNames.HasScope, PolicyNames.HasRoleAccessToContext);
    }

    public override async Task HandleAsync(
        CreateToDoItemDto request,
        CancellationToken ct)
    {
        var response = await _mediator.Send(new CreateToDoItemCommand(request), ct);

        await SendCreatedAtAsync<GetEndpoint>(
            new { Id = response.Value },
        response);
    }
}