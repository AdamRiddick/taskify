namespace Taskify.Web.Endpoints.Identity.UserContextRoles.Create;
using Taskify.Identity.UseCases.UserContextRoles.Create;

using FluentValidation;

using Taskify.Identity.Core.ContextTypeAggregate;
using Taskify.Identity.Core.UserAggregate;
using Taskify.SharedKernel.Data;

public class CreateValidator : AbstractValidator<CreateUserContextRoleCommand>
{
    public CreateValidator(
        IReadRepository<ContextType> contextTypeRepository,
        IReadRepository<User> userRepository)
    {
        RuleFor(x => x.Dto).NotNull();
        RuleFor(x => x.Dto.Role).IsInEnum();
        RuleFor(x => x.Dto)
            .MustAsync(async (dto, token) =>
            {
                var existingContextType = await contextTypeRepository.GetByIdAsync(dto.ContextTypeId);
                var existingUser = await userRepository.GetByIdAsync(dto.UserId);
                return existingContextType != null && existingUser != null;
            });
    }
}
