using Microsoft.EntityFrameworkCore;
using Moq;
using NSubstitute;
using NUnit.Framework;
using School.Core.Models;
using School.Core.Repositories;
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
            var options = new DbContextOptionsBuilder<SchoolDbContext>()
                .UseInMemoryDatabase("test")
                .Options;

            var context = Substitute.For<SchoolDbContext>(options);
            setDbSet(context, new List<Student>());
            var repository = Substitute.For<StudentRepository>(context);

            //var fakeUnitOfWork = Substitute.For<UnitOfWork>(context);

            //await repository.AddAsync(new Student());            
            //await fakeUnitOfWork.CommitAsync();
            var students = await repository.GetAllAsync();
            
            //Assert.AreEqual(1, context.Set<Student>());
            Assert.AreEqual(1, students.Count());
            
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

        public static void setDbSet<T>(DbContext dbContext, IEnumerable<T> data = null)
            where T : class
        {
            var dbSet = Substitute.For<DbSet<T>, IQueryable<T>>();

            if (data != null)
            {
                var queryable = data.AsQueryable();
                ((IQueryable<T>)dbSet).Provider.Returns(queryable.Provider);
                ((IQueryable<T>)dbSet).Expression.Returns(queryable.Expression);
                ((IQueryable<T>)dbSet).ElementType.Returns(queryable.ElementType);
                ((IQueryable<T>)dbSet).GetEnumerator().Returns(queryable.GetEnumerator());
                ((IQueryable<T>)dbSet).AsNoTracking().Returns(queryable);
            }

            dbContext.Set<T>().Returns(dbSet);
        }
    }
}
