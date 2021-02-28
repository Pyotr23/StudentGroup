using Microsoft.EntityFrameworkCore;
using School.Core.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace School.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected DbContext Context { get; private set; }

        public Repository(DbContext context)
        {
            Context = context;
            if (Context.ChangeTracker != null)
                Context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public async Task AddAsync(TEntity entity)
        {            
            await Context.Set<TEntity>().AddAsync(entity);
        }

        public void Remove(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }

        public ValueTask<TEntity> GetByIdAsync(int id)
        {
            return Context.Set<TEntity>().FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await Context.Set<TEntity>().ToListAsync();
        }

        public void Attach(TEntity entity)
        {
            Context.Set<TEntity>().Attach(entity);
        }
    }
}
