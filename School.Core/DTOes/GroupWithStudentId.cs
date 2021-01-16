namespace School.Core.DTOes
{
    public record GroupWithStudentId
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public int? StudentId { get; init; }
    }
}
