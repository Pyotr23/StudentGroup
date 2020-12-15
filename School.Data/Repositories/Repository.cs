using Microsoft.EntityFrameworkCore;
using School.Core.Repositories;
using System.Threading.Tasks;

namespace School.Data.Repositories
{
    public class Repository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected DbContext Context { get; private set; }

        public Repository(DbContext context)
        {
            Context = context;
        }

        public async Task AddAsync(TEntity entity)
        {
            await Context.Set<TEntity>().AddAsync(entity);
        }

        public void Remove(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }
    }
}
