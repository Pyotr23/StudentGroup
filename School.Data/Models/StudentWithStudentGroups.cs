using School.Core.Models;
using System.Collections.Generic;

namespace School.Data.Models
{
    public record StudentWithStudentGroups
    {
        public Student Student { get; init; }
        public IEnumerable<StudentGroup> StudentGroups { get; init; }
    }
}
