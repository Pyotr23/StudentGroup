using Microsoft.EntityFrameworkCore;
using School.Core.Models;
using School.Data.Configurations;

namespace School.Data
{
    public class SchoolDbContext : DbContext
    {
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Group> Groups { get; set; }

        public SchoolDbContext(DbContextOptions<SchoolDbContext> options)
            : base(options)
        { }

        public SchoolDbContext() { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .ApplyConfiguration(new StudentConfiguration())
                .ApplyConfiguration(new GroupConfiguration());
        }
    }
}
