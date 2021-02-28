using Microsoft.EntityFrameworkCore;
using Moq;
using NSubstitute;
using NUnit.Framework;
using School.Core.Models;
using School.Data.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School.Data.Tests.Unit.Repositories
{
    [TestFixture]
    public class StudentRepositoryTests
    {
        [Test]
        public async Task AddAsync()
        {
            //var studentRepository = Substitute.For<StudentRepository>();
            //var student = new Student() 
            //{
            //    Id = 1
            //};

            //await studentRepository.AddAsync(student);

            //Assert.AreSame(student, await studentRepository.GetByIdAsync(1));



            //var students = new List<Student>().AsQueryable();

            //var mockSet = Substitute.For<DbSet<Student>, IQueryable<Student>>();

            //// setup all IQueryable methods using what you have from "data"
            //((IQueryable<Student>)mockSet).Provider.Returns(students.Provider);
            //((IQueryable<Student>)mockSet).Expression.Returns(students.Expression);
            //((IQueryable<Student>)mockSet).ElementType.Returns(students.ElementType);
            //((IQueryable<Student>)mockSet).GetEnumerator().Returns(students.GetEnumerator());
            //var options = Substitute.For<DbContextOptions<SchoolDbContext>>();
            //var dbContextMock = Substitute.For<SchoolDbContext>(options);
            //dbContextMock.Set<Student>().Returns(mockSet);

            //// Act
            //var actors = Substitute.For<StudentRepository>(dbContextMock);
            //var data = await actors.GetStudentsAsync(new Core.Filtration.Parameters.StudentFilterParameters());

            //// Assert
            //Assert.IsTrue(data.Count() == 0);



            //var students = new List<Student>();
            ////var options = Substitute.For<DbContextOptions<SchoolDbContext>>();
            ////var options = new DbContextOptions<SchoolDbContext>();
            //var context = Substitute.For<SchoolDbContext>();
            //context.Students.AddRange(students);
            //var studentRepository = new StudentRepository(context);
            //var student = new Student()
            //{
            //    Id = 1
            //};

            //await studentRepository.AddAsync(student);

            //Assert.AreSame(student, await studentRepository.GetByIdAsync(1));

            var dbMoq = new Mock<SchoolDbContext>();
            dbMoq.Setup(p => p.Students).Returns(GetQueryableMockDbSet(new List<Student>()));            
            var context = dbMoq.Object;

            var repository = new Mock<StudentRepository>(context).Object;

            await repository.AddAsync(new Student());

            Assert.AreEqual(1, context.Students.Count());
        }

        public DbSet<T> GetQueryableMockDbSet<T>(List<T> sourceList) where T : class
        {
            var queryable = sourceList.AsQueryable();
            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            dbSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>((s) => sourceList.Add(s));
            return dbSet.Object;
        }
    }
}
