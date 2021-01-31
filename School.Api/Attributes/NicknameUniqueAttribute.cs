using School.Core.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace School.Api.Attributes
{
    public class NicknameUniqueAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            var studentService = (IStudentsService)validationContext
                         .GetService(typeof(IStudentsService));

            var nickname = value.ToString();
            return studentService.IsUniqueNicknameAsync(nickname).Result
                ? ValidationResult.Success
                : new ValidationResult($"Nickname {nickname} is already in use.");           
        }        
    }
}
