using Microsoft.EntityFrameworkCore;
using School.Core.DTOes;
using School.Core.Models;
using School.Core.Repositories;
using School.Data.Models;
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

        public async Task<GroupWithStudentCount> GetGroupWithStudentIdAsync(int id)
        {            
            return await SchoolDbContext
                .Groups
                .GroupJoin(SchoolDbContext.StudentGroups,
                    g => g,
                    sg => sg.Group,                    
                    (g, sges) => new GroupWithStudentGroups(g, sges))                   
                .SelectMany(gsges => gsges.StudentGroups.DefaultIfEmpty(),
                    (gsges, sg) => new GroupWithStudentId(gsges.Group, sg))     
                .GroupBy(x => new Group 
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .Select(grouped => new GroupWithStudentCount(grouped.Key, grouped.ToList()))
                .FirstOrDefaultAsync(g => g.Id == id);                   
        }
    }
}
