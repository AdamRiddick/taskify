namespace Taskify.Web.Endpoints.Tasks.ToDoItems;

using FastEndpoints;

using MediatR;

using Taskify.Tasks.UseCases.ToDoItems.Get;

public sealed class GetEndpoint : Endpoint<GetToDoItemQuery, GetToDoItemDto>
{
    private readonly IMediator _mediator;

    public GetEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        AllowAnonymous();
        Get("api/tasks/todoitem/{id}");
    }

    public override async Task HandleAsync(
        GetToDoItemQuery request,
        CancellationToken ct)
    {
        var response = await _mediator.Send(request, ct);
        await SendAsync(response.Value);
    }
}