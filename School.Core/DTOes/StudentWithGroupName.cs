using School.Core.Models;

namespace School.Core.DTOes
{
    public record StudentWithGroupName
    {
        public Student Student { get; init; }
        public string GroupName { get; init; }
    }
}
