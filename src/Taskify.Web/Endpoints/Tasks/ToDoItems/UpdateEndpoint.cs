namespace Taskify.Web.Endpoints.Tasks.ToDoItems;

using Ardalis.Result;

using FastEndpoints;

using MediatR;

using Taskify.Tasks.UseCases.ToDoItems.Update;

public sealed class UpdateEndpoint : Endpoint<UpdateToDoItemDto, Result>
{
    private readonly IMediator _mediator;

    public UpdateEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        AllowAnonymous();
        Put("api/tasks/todoitems/{id}");
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