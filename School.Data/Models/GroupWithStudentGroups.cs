using School.Core.Models;
using System.Collections.Generic;

namespace School.Data.Models
{
    public record GroupWithStudentGroups
    {
        public Group Group { get; init; }
        public IEnumerable<StudentGroup> StudentGroups { get; init; }
    }
}
