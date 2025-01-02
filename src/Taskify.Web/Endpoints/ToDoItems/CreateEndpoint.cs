namespace Taskify.Web.Endpoints.ToDoItems
{
    using FastEndpoints;

    using MediatR;

    using Taskify.Tasks.UseCases.ToDoItems.Create;

    public sealed class CreateEndpoint : Endpoint<CreateToDoItemDto, int>
    {
        private readonly IMediator _mediator;

        public CreateEndpoint(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override void Configure()
        {
            AllowAnonymous();
            Post("api/tasks/todoitems");
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
}