using System.Collections.Generic;

namespace StudentGroup.Infrastracture.Data.Models
{
    public class GroupWithGroupStudents
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<GroupStudent> GroupStudents { get; set; }
    }
}
