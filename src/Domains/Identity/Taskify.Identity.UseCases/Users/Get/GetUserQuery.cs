namespace Taskify.Identity.UseCases.Users.Get;

using Ardalis.Result;
using Taskify.SharedKernel.Cqrs;

public record GetUserQuery(int Id) : IQuery<Result<GetUserDto>>;
