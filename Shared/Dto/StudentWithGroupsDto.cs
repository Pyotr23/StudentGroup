using StudentGroup.Infrastracture.Data.Models;
using System.Collections.Generic;

namespace StudentGroup.Infrastracture.Shared.Dto
{
    public class StudentWithGroupsDto
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string Nickname { get; set; }
        public IEnumerable<Group> Groups { get; set; }
    }
}
