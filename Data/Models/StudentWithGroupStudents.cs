using StudentGroup.Infrastracture.Data.Models.Database;
using System.Collections.Generic;

namespace StudentGroup.Infrastracture.Data.Models
{
    public class StudentWithGroupStudents
    {
        public Student Student { get; set; }
        public IEnumerable<GroupStudent> GroupStudents {get; set;}
    }
}
