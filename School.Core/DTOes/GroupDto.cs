namespace School.Core.DTOes
{
    public record GroupDto
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public int StudentCount { get; init; }
    }
}
