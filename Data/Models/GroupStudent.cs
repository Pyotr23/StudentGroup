using System;
using System.Collections.Generic;

#nullable disable

namespace StudentGroup.Infrastracture.Data.Models
{
    public partial class GroupStudent
    {
        public int GroupId { get; set; }
        public int StudentId { get; set; }

        public virtual Group Group { get; set; }
        public virtual Student Student { get; set; }
    }
}
