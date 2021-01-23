using System.Collections.Generic;

namespace School.Core.DTOes
{
    public record StudentWithGroupNames
    {
        public int Id { get; init; }
        public string Sex { get; init; }
        public string LastName { get; init; }
        public string Name { get; init; }
        public string MiddleName { get; init; }
        public string Nickname { get; init; }
        public IEnumerable<string> GroupNames { get; init; }
    }
}
