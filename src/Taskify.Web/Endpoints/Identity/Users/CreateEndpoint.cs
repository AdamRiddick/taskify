namespace Taskify.Web.Endpoints.Identity.Users;

using FastEndpoints;

using MediatR;

using Taskify.Identity.UseCases.Users.Create;

public sealed class CreateEndpoint : Endpoint<CreateUserDto, int>
{
    private readonly IMediator _mediator;

    public CreateEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        AllowAnonymous();
        Post("api/identity/users");
    }

    public override async Task HandleAsync(
        CreateUserDto request,
        CancellationToken ct)
    {
        var response = await _mediator.Send(new CreateUserCommand(request), ct);

        await SendCreatedAtAsync<GetEndpoint>(
            new { Id = response.Value },
        response);
    }
}