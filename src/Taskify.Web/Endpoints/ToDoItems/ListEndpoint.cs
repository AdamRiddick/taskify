namespace Taskify.Web.Endpoints.ToDoItems
{
    using Ardalis.Result;

    using FastEndpoints;

    using MediatR;

    using Taskify.Tasks.UseCases.ToDoItems.List;

    public sealed class ListEndpoint : Endpoint<ListToDoItemsQuery, Result<IEnumerable<ListToDoItemDto>>>
    {
        private readonly IMediator _mediator;

        public ListEndpoint(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override void Configure()
        {
            AllowAnonymous();
            Get("api/todoitems");
        }

        public override async Task HandleAsync(
            ListToDoItemsQuery request,
            CancellationToken ct)
        {
            var response = await _mediator.Send(request, ct);
            await SendAsync(response);
        }
    }
}