using Microsoft.EntityFrameworkCore;
using School.Core.Repositories;
using System.Threading.Tasks;

namespace School.Data.Repositories
{
    public class Repository<TEntity> : BaseRepository<TEntity>, IRepository<TEntity> where TEntity : class
    {
        public Repository(DbContext context) : base(context)
        { }
        
        public async ValueTask<TEntity> GetByIdAsync(int id)
        {
            return await DbSet.FindAsync(id);
        }

        public ValueTask<TEntity> Update(TEntity entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
