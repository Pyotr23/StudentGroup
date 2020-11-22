namespace StudentGroup.Services.Api.Models
{
    public class StudentWithGroups
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string Nickname { get; set; }
        public string GroupNames { get; set; } 
    }
}
