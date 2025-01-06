namespace Taskify.Identity.UseCases.UserContextRoles.Delete;

using Ardalis.Result;

using Taskify.SharedKernel.Cqrs;

public record DeleteUserContextRoleCommand(int Id) : ICommand<Result>;
