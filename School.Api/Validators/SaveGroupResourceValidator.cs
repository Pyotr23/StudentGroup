﻿using FluentValidation;
using School.Api.Resources.GroupResources;

namespace School.Api.Validators
{
    public class SaveGroupResourceValidator : AbstractValidator<SaveGroupResource>
    {
        public SaveGroupResourceValidator()
        {            
            RuleFor(g => g.Name)
                .NotEmpty()
                .MaximumLength(25);            
        }
    }
}
