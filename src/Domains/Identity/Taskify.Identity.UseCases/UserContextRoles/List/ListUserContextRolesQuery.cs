namespace Taskify.Identity.UseCases.UserContextRoles.List;

using Ardalis.Result;

using Taskify.SharedKernel.Cqrs;

public record ListUserContextRolesQuery() : IQuery<Result<IEnumerable<ListUserContextRolesDto>>>;
