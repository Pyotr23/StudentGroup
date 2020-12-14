using Microsoft.EntityFrameworkCore;
using School.Core.Repositories;
using System.Threading.Tasks;

namespace School.Data.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        public virtual DbSet<TEntity> DbSet { get; private set; }

        public BaseRepository(DbContext context)
        {
            DbSet = context.Set<TEntity>();
        }

        public async Task AddAsync(TEntity entity)
        {
            await DbSet.AddAsync(entity);
        }

        public void Remove(TEntity entity)
        {
            DbSet.Remove(entity);
        }
    }
}
