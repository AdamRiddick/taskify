namespace Taskify.Web.Endpoints.Tasks.ToDoItems;

using Ardalis.Result;

using FastEndpoints;

using MediatR;

using Taskify.Tasks.UseCases.ToDoItems.Delete;

public sealed class DeleteEndpoint : Endpoint<DeleteToDoItemCommand, Result>
{
    private readonly IMediator _mediator;

    public DeleteEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        AllowAnonymous();
        Delete("api/tasks/todoitems/{id}");
    }

    public override async Task HandleAsync(
        DeleteToDoItemCommand request,
        CancellationToken ct)
    {
        var response = await _mediator.Send(request, ct);
        await SendAsync(response);
    }
}