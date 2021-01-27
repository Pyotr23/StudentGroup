using System.Collections.Generic;

namespace School.Core.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Sex { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string Nickname { get; set; }
        public ICollection<Group> Groups { get; set; } = new List<Group>();
    }
}
