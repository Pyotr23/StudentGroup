using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using School.Core.Models;

namespace School.Data.Configurations
{
    public class StudentGroupConfiguration : IEntityTypeConfiguration<StudentGroup>
    {
        public void Configure(EntityTypeBuilder<StudentGroup> builder)
        {
            builder
                .HasKey(sg => new { sg.StudentId, sg.GroupId });

            builder
                .HasOne(sg => sg.Student)
                .WithMany(s => s.StudentGroups)
                .HasForeignKey(sg => sg.StudentId);

            builder
                .HasOne(sg => sg.Group)
                .WithMany(g => g.StudentGroups)
                .HasForeignKey(sg => sg.GroupId);
        }
    }
}
