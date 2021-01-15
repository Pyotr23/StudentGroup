using Microsoft.EntityFrameworkCore;
using School.Core.DTOes;
using School.Core.Models;
using School.Core.Repositories;
using System.Linq;
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

        public async Task<GroupWithStudentCount> GetGroupWithStudentCountAsync(int id)
        {
            return await SchoolDbContext
                .Groups
                .GroupJoin(SchoolDbContext.StudentGroups,
                    g => g.Id,
                    sg => sg.GroupId,
                    (g, sges) => new GroupWithStudentCount
                    {
                        Id = g.Id,
                        Name = g.Name,
                        StudentCount = sges.Select(s => s.StudentId).Count()
                    }
                )
                .FirstOrDefaultAsync(g => g.Id == id);                
        }
    }
}
