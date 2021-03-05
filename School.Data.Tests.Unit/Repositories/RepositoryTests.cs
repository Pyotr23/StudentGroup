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
        private const string TestName = "TestName";
        private const string TestLastName = "TestLastName";
        private const string TestMiddleName = "TestMiddleName";
        private const string TestNickname = "TestNickname";
        private const string TestSex = "TestSex";

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

        [Test]
        public async Task AddAsync_StudentNotNull_PropertiesAreEqual()
        {
            var students = new List<Student> {
                new()
                {
                    Id = 1
                }
            };
            var context = await GetMockContextAsync(students);
            IStudentRepository repo = new StudentRepository(context);
            var studentForAdding = new Student
            {
                Name = TestName,
                LastName = TestLastName,
                MiddleName = TestMiddleName,
                Nickname = TestNickname,
                Sex = TestSex
            };

            await repo.AddAsync(studentForAdding);
            await context.SaveChangesAsync();

            var addedStudent = context
                .Students
                .AsEnumerable()
                .LastOrDefault();

            Assert.AreEqual(TestName, addedStudent.Name);
            Assert.AreEqual(TestLastName, addedStudent.LastName);
            Assert.AreEqual(TestMiddleName, addedStudent.MiddleName);
            Assert.AreEqual(TestNickname, addedStudent.Nickname);
            Assert.AreEqual(TestSex, addedStudent.Sex);
        }

        [Test]
        public async Task AddAsync_StudentNotNull_IncrementedId()
        {
            var students = new List<Student> 
            {
                new()
                {
                    Id = 2
                }
            };
            var context = await GetMockContextAsync(students);
            IStudentRepository repo = new StudentRepository(context);
            var studentForAdding = new Student();

            await repo.AddAsync(studentForAdding);
            await context.SaveChangesAsync();

            var addedStudent = context
                .Students
                .AsEnumerable()
                .LastOrDefault();

            Assert.AreEqual(3, addedStudent.Id);
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
