namespace Taskify.Identity.UseCases.UserContextRoles.Verify;

using Ardalis.Result;

using Taskify.SharedKernel.Cqrs;

public record VerifyUserContextRoleQuery(VerifyUserContextRoleDto Dto) : IQuery<Result<bool>>;