using StudentGroup.Infrastracture.Data.Models;

namespace Data.Models
{
    public class GroupStudent
    {
        public int GroupId { get; set; }
        public Group Group { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
    }
}
