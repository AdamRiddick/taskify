namespace Taskify.Identity.UseCases.Users.List;

using Ardalis.Result;

using Taskify.SharedKernel.Cqrs;

public record ListUsersQuery() : IQuery<Result<IEnumerable<ListUsersDto>>>;
