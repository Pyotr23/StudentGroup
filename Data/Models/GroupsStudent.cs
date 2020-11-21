using System;
using System.Collections.Generic;

#nullable disable

namespace WebApi.Models
{
    public partial class GroupsStudent
    {
        public int GroupId { get; set; }
        public int StudentId { get; set; }

        public virtual Group Group { get; set; }
        public virtual Student Student { get; set; }
    }
}
