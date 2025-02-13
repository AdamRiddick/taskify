namespace Taskify.Api.Endpoints.Identity.UserContextRoles.Create;

using Taskify.Identity.UseCases.UserContextRoles.Create;

using FluentValidation;

using Taskify.Identity.Core.ContextTypeAggregate;
using Taskify.Identity.Core.UserAggregate;
using Taskify.SharedKernel.Data;
using Taskify.Identity.Core.UserContextRoleAggregate;

public class CreateValidator : AbstractValidator<CreateUserContextRoleCommand>
{
    public CreateValidator(
        IReadRepository<ContextType> contextTypeRepository,
        IReadRepository<User> userRepository,
       IReadRepository<UserContextRole> userContextRoleRepository)
    {
        RuleFor(x => x.Dto).NotNull();
        RuleFor(x => x.Dto.Role).IsInEnum();
        RuleFor(x => x.Dto)
            .MustAsync(async (dto, token) =>
            {
                var existingUser = await userRepository.GetByIdAsync(dto.UserId, token);
                return existingUser != null;
            })
            .WithMessage("User does not exist.")
            .MustAsync(async (dto, token) =>
            {
                var existingContextType = await contextTypeRepository.GetByIdAsync(dto.ContextTypeId, token);
                return existingContextType != null;
            })
            .WithMessage("Context Type does not exist.")
            .MustAsync(async (dto, token) =>
            {
                var existingUserContextRole = 
                    await userContextRoleRepository.GetAsync(
                        ucr => ucr.ContextId.Equals(dto.ContextId)
                            && ucr.Role.Equals(dto.Role)
                            && ucr.UserId.Equals(dto.UserId), token);
                return existingUserContextRole == null;
            })
            .WithMessage("User Context Role Already Exists.");
    }
}
