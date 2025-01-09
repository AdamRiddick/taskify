namespace Taskify.Web.Endpoints.Identity.UserContextRoles.Delete;

using FluentValidation;

using Taskify.Identity.Core.UserContextRoleAggregate;
using Taskify.Identity.UseCases.UserContextRoles.Delete;
using Taskify.SharedKernel.Data;

public class DeleteValidator : AbstractValidator<DeleteUserContextRoleCommand>
{
    public DeleteValidator(
        IRepository<UserContextRole> repository)
    {
        RuleFor(x => x.Id).NotEmpty();

        RuleFor(x => x)
            .MustAsync(async (x, token) =>
            {
                var existingEntity = await repository.GetByIdAsync(x.Id, token);
                return existingEntity != null;
            });
    }
}
