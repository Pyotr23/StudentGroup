namespace School.Core.DTOes
{
    public record StudentWithGroupsDto
    {
        public int Id { get; init; }
        public string Sex { get; init; }
        public string LastName { get; init; }
        public string Name { get; init; }
        public string MiddleName { get; init; }
        public string Nickname { get; init; }
        public string GroupNamesToString { get; init; }
    }
}
