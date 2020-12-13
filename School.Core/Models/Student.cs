﻿using System.Collections.Generic;

namespace School.Core.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Sex { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string NickName { get; set; }
        public ICollection<StudentGroup> StudentGroups { get; set; }
    }
}