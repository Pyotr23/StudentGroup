using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using School.Core.Models;

namespace School.Data.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder
                .HasKey(s => s.Id);

            builder
                .Property(s => s.Id);

            builder
                .Property(s => s.Sex)
                .IsRequired();

            builder
                .Property(s => s.LastName)
                .IsRequired()
                .HasMaxLength(40);

            builder
                .Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(40);

            builder
                .Property(s => s.MiddleName)
                .HasMaxLength(60);

            builder
                .Property(s => s.Nickname)
                .HasMaxLength(16);            
        }
    }
}
