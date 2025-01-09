namespace Taskify.Identity.UseCases.UserContextRoles.Verify;

using FluentValidation;

using Taskify.Identity.Core.ContextTypeAggregate;
using Taskify.Identity.Core.UserAggregate;
using Taskify.SharedKernel.Data;

public class VerifyUserContextRoleValidator : AbstractValidator<VerifyUserContextRoleQuery>
{
    public VerifyUserContextRoleValidator(
        IReadRepository<ContextType> contextTypeRepository,
        IReadRepository<User> userRepository)
    {
        RuleFor(x => x.Dto).NotNull();
        RuleFor(x => x.Dto.Role).IsInEnum();
        RuleFor(x => x.Dto.ContextType).NotNull().NotEmpty();
        RuleFor(x => x.Dto)
            .MustAsync(async (dto, token) =>
            {
                var existingContextType = await contextTypeRepository.GetAsync(x => x.Name.Equals(x.Name, StringComparison.InvariantCultureIgnoreCase));
                var existingUser = await userRepository.GetByIdAsync(dto.UserId);
                return existingContextType != null && existingUser != null;
            });
    }
}
