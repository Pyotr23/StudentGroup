using System;
using System.Collections.Generic;
using System.Text;

namespace StudentGroup.Infrastracture.Shared.Dto
{
    public class StudentDto
    {
        public int Id { get; set; }
        public string Sex { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string Nickname { get; set; }
    }
}
