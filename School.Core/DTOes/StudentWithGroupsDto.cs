namespace School.Core.DTOes
{
    public class StudentWithGroupsDto
    {
        public int Id { get; set; }
        public string Sex { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string Nickname { get; set; }
        public string GroupNamesToString { get; set; }
    }
}
