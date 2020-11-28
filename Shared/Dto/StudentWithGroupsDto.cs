namespace StudentGroup.Infrastracture.Shared.Dto
{
    public class StudentWithGroupsDto
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string Nickname { get; set; }
        public string GroupNamesString { get; set; }
    }
}
