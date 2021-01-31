using FluentValidation;
using School.Api.Resources.StudentResources;
using School.Core.Services;
using System.Threading.Tasks;

namespace School.Api.Validators
{
    public class SaveStudentResourceValidator : AbstractValidator<SaveStudentResource>
    {
        private readonly IStudentsService _studentsService;
        public SaveStudentResourceValidator(IStudentsService studentsService)
        {
            _studentsService = studentsService;

            RuleFor(s => s.Sex)
                .NotEmpty();

            RuleFor(s => s.LastName)
                .NotEmpty()
                .MaximumLength(40);

            RuleFor(s => s.Name)
                .NotEmpty()
                .MaximumLength(40);

            RuleFor(s => s.MiddleName)                
                .MaximumLength(60);

            RuleFor(s => s.Nickname)
                .MinimumLength(6)
                .MaximumLength(16)
                .MustAsync((nickname, c) => _studentsService.IsUniqueNicknameAsync(nickname))
                .WithMessage(n => $"Студент с 'Nickname' = '{n.Nickname}' уже существует.");
        }        
    }
}
