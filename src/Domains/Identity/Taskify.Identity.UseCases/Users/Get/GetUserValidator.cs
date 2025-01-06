namespace Taskify.Identity.UseCases.Users.Get;

using FluentValidation;

using Taskify.Identity.Core.UserAggregate;
using Taskify.SharedKernel.Data;

public class GetToDoItemValidator : AbstractValidator<GetUserQuery>
{
    public GetToDoItemValidator(
        IReadRepository<User> repository)
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
