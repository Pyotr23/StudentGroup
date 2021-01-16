using Microsoft.EntityFrameworkCore;
using School.Core.DTOes;
using School.Core.Models;
using School.Core.Repositories;
using System.Collections.Generic;
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

        public async Task<IEnumerable<GroupWithStudentId>> GetGroupWithStudentIdAsync(int id)
        {
            //        var data = ctx
            //.MyTable1
            //.SelectMany(a => ctx.MyTable2
            //  .Where(b => b.Id2 == a.Id1)
            //  .DefaultIfEmpty()
            //  .Select(b => new
            //  {
            //      a.Id1,
            //      a.Col1,
            //      Col2 = b == null ? (int?)null : b.Col2,
            //  }));
            return await SchoolDbContext
                .Groups
                .GroupJoin(SchoolDbContext.StudentGroups,
                    g => g,
                    sg => sg.Group,                    
                    (g, sg) => new { g, sg })
                .SelectMany(x => x.sg.DefaultIfEmpty(),
                    (one, two) => new GroupWithStudentId { Id = one.g.Id, Name = one.g.Name, StudentId = two == default ? (int?)null : two.StudentId }
                )                
                .ToListAsync();
            //return await SchoolDbContext
            //    .Groups
            //    .Join(SchoolDbContext.StudentGroups,
            //        g => g.Id,
            //        sg => sg.GroupId,
            //        (g, sg) => new GroupWithStudentId
            //        {
            //            Id = g.Id,
            //            Name = g.Name,
            //            StudentId = sg.StudentId
            //        }
            //    )
            //    //.Where(g => g.StudentId == id)
            //    .ToListAsync();                                
        }

        public Task<GroupWithStudentId> GetGroupWithStudentCountAsync(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
