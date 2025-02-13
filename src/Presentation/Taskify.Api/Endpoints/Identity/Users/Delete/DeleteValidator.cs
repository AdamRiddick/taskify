namespace Taskify.Api.Endpoints.Identity.Users.Delete;

using FluentValidation;

using Taskify.Identity.Core.UserAggregate;
using Taskify.Identity.UseCases.Users.Delete;
using Taskify.SharedKernel.Data;

public class DeleteValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteValidator(
        IRepository<User> repository)
    {
        RuleFor(x => x.Id).NotEmpty();

        RuleFor(x => x)
            .MustAsync(async (x, token) =>
            {
                var existingEntity = await repository.GetByIdAsync(x.Id, token);
                return existingEntity != null;
            })
            .WithMessage("Entity does not exist.");
    }
}
