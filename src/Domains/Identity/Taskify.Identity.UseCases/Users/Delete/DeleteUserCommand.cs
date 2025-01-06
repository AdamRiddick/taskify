namespace Taskify.Identity.UseCases.Users.Delete;

using Ardalis.Result;

using Taskify.SharedKernel.Cqrs;

public record DeleteUserCommand(int Id) : ICommand<Result>;
