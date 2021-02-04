using School.Core.Models;

namespace School.Core.DTOes
{
    public record GroupWithStudentCount
    {
        //public int Id { get; init; }
        //public string Name { get; init; }
        public Group Group { get; init; }
        public int StudentCount { get; init; }
    }
}
