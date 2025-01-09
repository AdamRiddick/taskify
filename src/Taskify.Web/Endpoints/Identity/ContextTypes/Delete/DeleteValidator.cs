namespace Taskify.Web.Endpoints.Identity.ContextTypes.Delete;
using Taskify.Identity.UseCases.ContextTypes.Delete;

using FluentValidation;

using Taskify.Identity.Core.ContextTypeAggregate;
using Taskify.SharedKernel.Data;

public class DeleteValidator : AbstractValidator<DeleteContextTypeCommand>
{
    public DeleteValidator(
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
