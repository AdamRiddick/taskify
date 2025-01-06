namespace Taskify.Identity.UseCases.Users.Create;

using Ardalis.Result;

using Taskify.SharedKernel.Cqrs;

public record CreateUserCommand(CreateUserDto Dto) : ICommand<Result<int>>;