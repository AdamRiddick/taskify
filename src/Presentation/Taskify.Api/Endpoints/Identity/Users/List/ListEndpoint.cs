﻿namespace Taskify.Api.Endpoints.Identity.Users.List;

using Ardalis.Result;

using FastEndpoints;

using MediatR;

using Taskify.Api.Authorization;
using Taskify.Identity.UseCases.Users.List;
using Taskify.SharedKernel.Security;
using Taskify.Api.Authorization.Attributes;

[ContextAuthorization(SecurityContexts.Identity.Users, Role.Reader)]
[ScopeAuthorization(Scopes.Identity.Users.All, Scopes.Identity.Users.Read)]
public sealed class ListEndpoint : Endpoint<ListUsersQuery, Result<IEnumerable<ListUsersDto>>>
{
    private readonly IMediator _mediator;

    public ListEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Get("api/tasks/users");
        Policies(PolicyNames.HasScope, PolicyNames.HasRoleAccessToContext);
    }

    public override async Task HandleAsync(
        ListUsersQuery request,
        CancellationToken ct)
    {
        var response = await _mediator.Send(request, ct);
        await SendAsync(response);
    }
}