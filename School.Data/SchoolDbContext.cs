using Microsoft.EntityFrameworkCore;
using School.Core.Models;
using School.Data.Configurations;

namespace School.Data
{
    public class SchoolDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Group> Groups { get; set; }

        public SchoolDbContext(DbContextOptions<SchoolDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .ApplyConfiguration(new StudentConfiguration())
                .ApplyConfiguration(new GroupConfiguration());
        }
    }
}
