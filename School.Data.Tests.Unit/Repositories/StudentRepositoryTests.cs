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
    public class StudentRepositoryTests
    {
        //[Test]
        //public async Task AddAsync()
        //{
        //    var dbContextMock = new Mock<SchoolDbContext>();
        //    var dbSetMock = new Mock<DbSet<Student>>();
        //    dbSetMock.Setup(s => s.(It.IsAny<int>())).Returns();
        //    dbContextMock.Setup(s => s.Set<Student>()).Returns(dbSetMock.Object);

        //    //Execute method of SUT (ProductsRepository)  
        //    var productRepository = new StudentRepository(dbContextMock.Object);
        //    var product = await productRepository.GetByIdAsync(6);

        //    //Assert  
        //    Assert.NotNull(product);
        //    Assert.IsAssignableFrom<Student>(product);

        //}

        [Test]
        public async Task BlogRepository_Searches_Posts_By_Authors_Surname()
        {
            IStudentRepository repo = new StudentRepository(GetMockContext());

            var posts =  await repo.GetByIdAsync(1);

            Assert.AreEqual(1, posts.Id);
        }

        [Test]
        public async Task BlogRepository_Searches_Posts_By_Authors_Surname2()
        {
            var context = GetMockContext();
            IStudentRepository repo = new StudentRepository(context);

            await repo.AddAsync(new()
            {
                Id = 2
            });
            context.SaveChanges();

            var res = await repo.GetByIdAsync(2);

            Assert.AreEqual(2, res.Id);
        }


        private SchoolDbContext GetMockContext()
        {
            var options = new DbContextOptionsBuilder<SchoolDbContext>()
                              .UseInMemoryDatabase(Guid.NewGuid().ToString())
                              .Options;

            var context = new SchoolDbContext(options);

            context.Students.Add(new()
            {
                Id = 1
            });

            context.SaveChanges();

            return context;
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
