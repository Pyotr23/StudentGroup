﻿using System.ComponentModel.DataAnnotations;

namespace School.Api.Resources.StudentResources
{
    public record SaveStudentResource
    {
        [Required]
        public string Sex { get; init; }

        [Required]
        [MaxLength(40)]
        public string LastName { get; init; }

        [Required]
        [MaxLength(40)]
        public string Name { get; init; }

        [MaxLength(60)]
        public string MiddleName { get; init; }

        [MinLength(6)]
        [MaxLength(16)]
        public string Nickname { get; init; }
    }
}