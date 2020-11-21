using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Contexts
{
    public class SchoolContext : DbContext
    {
        public SchoolContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server =.\SQLEXPRESS; Database = SchoolDB; Trusted_Connection = True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<GroupStudent>()
                .HasKey(sc => new { sc.GroupId, sc.StudentId });
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupStudent> GroupStudents { get; set; }
    }
}
