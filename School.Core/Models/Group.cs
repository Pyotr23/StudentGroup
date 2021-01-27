using System.Collections.Generic;

namespace School.Core.Models
{
    public class Group
    {        
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}
