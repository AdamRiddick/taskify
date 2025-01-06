namespace Taskify.Identity.UseCases.Users.Update;

using Ardalis.Result;

using Taskify.SharedKernel.Cqrs;

public record UpdateUserCommand(int Id, UpdateUserDto Dto) : ICommand<Result>;
