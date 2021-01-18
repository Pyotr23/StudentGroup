using FluentValidation;
using School.Api.Resources;

namespace School.Api.Validators
{
    public class SaveGroupResourceValidator : AbstractValidator<SaveGroupResource>
    {
        public SaveGroupResourceValidator()
        {
            RuleFor(g => g.Name)
                .NotEmpty()
                .MaximumLength(40);
        }
    }
}
