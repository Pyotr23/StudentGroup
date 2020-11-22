using System;
using System.Collections.Generic;

#nullable disable

namespace StudentGroup.Infrastracture.Data.Models
{
    public partial class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public ICollection<GroupStudent> GroupStudents { get; set; }
    }
}
