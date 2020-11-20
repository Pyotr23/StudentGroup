using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class Group
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [MaxLength(25)]
        public string Name { get; set; }

        public ICollection<GroupStudent> GroupStudents { get; set; }
    }
}
