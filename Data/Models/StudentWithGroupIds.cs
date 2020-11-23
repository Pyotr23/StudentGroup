using System.Collections.Generic;

namespace StudentGroup.Infrastracture.Data.Models
{
    public class StudentWithGroupIds
    {
        public Student Student { get; set; }
        public int? GroupId { get; set; }
    }
}
