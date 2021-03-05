using Microsoft.EntityFrameworkCore;
using Moq;
using NSubstitute;
using NUnit.Framework;
using School.Core.Models;
using School.Core.Repositories;
using School.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School.Data.Tests.Unit.Repositories
{
    [TestFixture]
    public class RepositoryTests
    {
        [Test]
        public async Task GetByIdAsync_StudentExists_GetTheSame()
        {
            var students = new List<Student>
            {
                new()
                {
                    Id = 1
                },
                new()
                {
                    Id = 2
                }
            };
            var context = await GetMockContextAsync(students);
            IStudentRepository repo = new StudentRepository(context);

            var student = await repo.GetByIdAsync(2);

            Assert.AreSame(students[1], student);
        }

        [Test]
        public async Task GetByIdAsync_StudentNotExists_GetNull()
        {
            var students = new List<Student>
            {
                new()
                {
                    Id = 1
                },
                new()
                {
                    Id = 2
                }
            };
            var context = await GetMockContextAsync(students);
            IStudentRepository repo = new StudentRepository(context);

            var student = await repo.GetByIdAsync(3);

            Assert.IsNull(student);
        }

        [Test]
        public async Task GetAllAsync_StudentsExist_GetRightCount()
        {
            var students = new List<Student>
            {
                new()
                {
                    Id = 1
                },
                new()
                {
                    Id = 2
                }
            };
            var context = await GetMockContextAsync(students);
            IStudentRepository repo = new StudentRepository(context);

            var receivedStudents = await repo.GetAllAsync();

            Assert.AreEqual(students.Count, receivedStudents.Count());
        }

        [Test]
        public async Task GetAllAsync_StudentsNotExist_EmptyList()
        {
            var students = new List<Student>();
            var context = await GetMockContextAsync(students);
            IStudentRepository repo = new StudentRepository(context);

            var receivedStudents = await repo.GetAllAsync();

            Assert.AreEqual(0, receivedStudents.Count());
        }

        private async Task<SchoolDbContext> GetMockContextAsync(IEnumerable<Student> students)
        {
            var options = new DbContextOptionsBuilder<SchoolDbContext>()
                              .UseInMemoryDatabase(Guid.NewGuid().ToString())
                              .Options;

            var context = new SchoolDbContext(options);

            await context.Students.AddRangeAsync(students);

            context.SaveChanges();

            return context;
        }
    }
}
