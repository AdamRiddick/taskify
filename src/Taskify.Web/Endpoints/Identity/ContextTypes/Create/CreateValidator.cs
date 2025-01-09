namespace Taskify.Web.Endpoints.Identity.ContextTypes.Create;
using Taskify.Identity.UseCases.ContextTypes.Create;

using FluentValidation;

using Taskify.Identity.Core.ContextTypeAggregate;
using Taskify.SharedKernel.Data;

public class CreateValidator : AbstractValidator<CreateContextTypeCommand>
{
    public CreateValidator(IRepository<ContextType> repository)
    {
        RuleFor(x => x.Dto).NotNull();
        RuleFor(x => x.Dto.Name).NotEmpty().MaximumLength(255);

        RuleFor(x => x.Dto)
            .MustAsync(async (dto, token) =>
            {
                var existing = await repository.GetAsync(x => x.Name == dto.Name);
                return existing == null;
            });
    }
}
