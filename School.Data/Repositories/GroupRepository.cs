using School.Core.Models;
using School.Core.Repositories;
using System.Threading.Tasks;

namespace School.Data.Repositories
{
    public class GroupRepository : Repository<Group>, IGroupRepository
    {
        private SchoolDbContext SchoolDbContext => Context as SchoolDbContext;

        public GroupRepository(SchoolDbContext context) : base(context)
        { }

        public async Task<Group> GetByIdAsync(int id)
        {
            return await SchoolDbContext
               .Groups
               .FindAsync(id);
        }
    }
}
