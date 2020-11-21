using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class Student
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        public string Sex { get; set; }

        [Required]
        [MaxLength(40)]
        public string Surname { get; set; }

        [Required]
        [MaxLength(40)]
        public string Name { get; set; }

        [MaxLength(60)]
        public string MiddleName { get; set; }               
        
        [MinLength(6)]
        [MaxLength(16)]        
        public string Nickname { get; set; }

        public ICollection<GroupStudent> GroupStudents { get; set; }
    }
}
