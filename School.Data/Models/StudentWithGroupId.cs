using School.Core.Models;

namespace School.Data.Models
{
    public record StudentWithGroupId
    {
        public Student Student { get; init; }
        public int? GroupId { get; init; }
    }
}
