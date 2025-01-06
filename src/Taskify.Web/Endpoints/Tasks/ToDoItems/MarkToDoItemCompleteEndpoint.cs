namespace Taskify.Web.Endpoints.Tasks.ToDoItems;

using Ardalis.Result;

using FastEndpoints;

using MediatR;

using Taskify.Tasks.UseCases.ToDoItems.MarkToDoItemComplete;

public sealed class MarkToDoItemCompleteEndpoint : Endpoint<MarkToDoItemCompleteCommand, Result>
{
    private readonly IMediator _mediator;

    public MarkToDoItemCompleteEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        AllowAnonymous();
        Post("api/tasks/todoitems/{id}/complete");

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