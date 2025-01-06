namespace Taskify.Identity.UseCases.ContextTypes.Delete;

using FluentValidation;

using Taskify.Identity.Core.ContextTypeAggregate;
using Taskify.SharedKernel.Data;

public class DeleteContextTypeValidator : AbstractValidator<DeleteContextTypeCommand>
{
    public DeleteContextTypeValidator(
        IRepository<ContextType> repository)
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
