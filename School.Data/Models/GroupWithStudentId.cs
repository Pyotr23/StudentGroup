namespace School.Data.Models
{
    public record GroupWithStudentId
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public int? StudentId { get; init; }
    }
}
