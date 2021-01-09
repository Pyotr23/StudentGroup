using FluentValidation;
using School.Api.Resources;

namespace School.Api.Validators
{
    public class SaveStudentResourceValidator : AbstractValidator<SaveStudentResource>
    {
        public SaveStudentResourceValidator()
        {
            RuleFor(s => s.Sex)
                .NotEmpty();

            RuleFor(s => s.Name)
                .NotEmpty()
                .MaximumLength(40);

            RuleFor(s => s.LastName)
                .NotEmpty()
                .MaximumLength(40);

            RuleFor(s => s.MiddleName)
                .MaximumLength(60);

            RuleFor(s => s.Nickname)
                .MinimumLength(6)
                .MaximumLength(16);
        }
    }
}
