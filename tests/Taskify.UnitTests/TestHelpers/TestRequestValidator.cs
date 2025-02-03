namespace Taskify.UnitTests.TestHelpers;

using FluentValidation;
using Taskify.UnitTests.TestHelpers.Objects;

public class TestRequestValidator : AbstractValidator<TestRequest>
{
    public TestRequestValidator(bool shouldFail = false)
    {
        RuleFor(x => x).NotNull();

        if (shouldFail)
        {
            RuleFor(x => x)
                .Custom((_, context) => context.AddFailure("Forced failure."));
        }
    }
}