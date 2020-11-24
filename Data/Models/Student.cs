using System;
using System.Collections.Generic;

#nullable disable

namespace StudentGroup.Infrastracture.Data.Models
{
    public partial class Student
    {
        public int Id { get; set; }
        public string Sex { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string Nickname { get; set; }
    }
}
