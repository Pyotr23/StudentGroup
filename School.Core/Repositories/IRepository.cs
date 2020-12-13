using System.Threading.Tasks;

namespace School.Core.Repositories
{
    public interface IRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        ValueTask<TEntity> GetByIdAsync(int id);        
        ValueTask<TEntity> Update(TEntity entity);
    }
}
