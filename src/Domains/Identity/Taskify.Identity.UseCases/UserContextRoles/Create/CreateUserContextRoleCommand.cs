namespace Taskify.Identity.UseCases.UserContextRoles.Create;

using Ardalis.Result;

using Taskify.SharedKernel.Cqrs;

public record CreateUserContextRoleCommand(CreateUserContextRoleDto Dto) : ICommand<Result<int>>;