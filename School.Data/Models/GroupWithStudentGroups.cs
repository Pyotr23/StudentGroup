using School.Core.Models;
using System.Collections.Generic;

namespace School.Data.Models
{
    public record GroupWithStudentGroups
    {
        public Group Group { get; init; }
        public IEnumerable<StudentGroup> StudentGroups { get; init; }

        public GroupWithStudentGroups(Group group, IEnumerable<StudentGroup> studentGroups) =>
            (Group, StudentGroups) = (group, studentGroups);
    }
}
