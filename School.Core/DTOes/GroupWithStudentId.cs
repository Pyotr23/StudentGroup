using School.Core.Models;

namespace School.Core.DTOes
{
    public record GroupWithStudentId
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public int? StudentId { get; init; }

        public GroupWithStudentId(Group group, StudentGroup studentGroup) =>
            (Id, Name, StudentId) = 
                (group.Id, group.Name, studentGroup.StudentId == default ? null : studentGroup.StudentId);
    }
}
