namespace StudentGroup.Infrastracture.Data.Models
{
    public class GroupWithStudentId
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? StudentId { get; set; }
    }
}
