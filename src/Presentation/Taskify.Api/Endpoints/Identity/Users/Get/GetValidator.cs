namespace Taskify.Api.Endpoints.Identity.Users.Get;
using Taskify.Identity.UseCases.Users.Get;

using FluentValidation;

using Taskify.Identity.Core.UserAggregate;
using Taskify.SharedKernel.Data;

public class GetValidator : AbstractValidator<GetUserQuery>
{
    public GetValidator(
        IReadRepository<User> repository)
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
