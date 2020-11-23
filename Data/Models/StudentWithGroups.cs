using System.Collections.Generic;

namespace StudentGroup.Infrastracture.Data.Models
{
    public class StudentWithGroups
    {
        public Student Student { get; set; }
        public IEnumerable<Group> Groups { get; set; }
    }
}
