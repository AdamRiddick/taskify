﻿namespace Taskify.Identity.UseCases.Users.Create;

using FluentValidation;

public class CreateUserValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.Dto).NotNull();
        RuleFor(x => x.Dto.EmailAddress)
            .NotNull()
            .MaximumLength(254)
            .EmailAddress();
        RuleFor(x => x.Dto.Name)
            .NotNull()
            .MinimumLength(3)
            .MaximumLength(100);
    }
}
