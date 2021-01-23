using School.Core.Models;

namespace School.Data.Models
{
    public record StudentWithStudentGroup
    {
        public Student Student { get; init; }
        public StudentGroup StudentGroup { get; init; }
    }
}
