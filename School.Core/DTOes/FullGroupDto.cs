namespace School.Core.DTOes
{
    public record FullGroupDto
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public int StudentCount { get; init; }
    }
}
