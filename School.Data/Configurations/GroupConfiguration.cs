using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using School.Core.Models;

namespace School.Data.Configurations
{
    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder
                .HasKey(g => g.Id);

            builder
                .Property(g => g.Id)
                .UseIdentityColumn();

            builder
                .Property(g => g.Name)
                .IsRequired()
                .HasMaxLength(25);

            builder
                .ToTable("Groups");
        }
    }
}
