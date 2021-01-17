using School.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace School.Core.DTOes
{
    public record GroupWithStudentCount
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public int StudentCount { get; init; }

        public GroupWithStudentCount(Group group, IEnumerable<GroupWithStudentId> groups) =>
            (Id, Name, StudentCount) = 
                (group.Id, group.Name, StudentCount = groups.Count(g => g.StudentId != null));
    }
}
