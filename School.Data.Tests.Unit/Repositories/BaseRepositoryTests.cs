using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace School.Data.Tests.Unit.Repositories
{
    public class BaseRepositoryTests<T> where T : class
    {
        public async Task<SchoolDbContext> GetMockContextAsync(IEnumerable<T> entities)
        {
            var options = new DbContextOptionsBuilder<SchoolDbContext>()
                              .UseInMemoryDatabase(Guid.NewGuid().ToString())
                              .Options;

            var context = new SchoolDbContext(options);

            await context.Set<T>().AddRangeAsync(entities);

            context.SaveChanges();

            return context;
        }
    }
}
