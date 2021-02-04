using Microsoft.EntityFrameworkCore;
using School.Core.DTOes;
using School.Core.Filtration.Filters;
using School.Core.Filtration.Parameters;
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

        public async Task<IEnumerable<GroupWithStudentCount>> GetGroupsAsync(GroupFilterParameters filterParameters)
        {
            var groups = await SchoolDbContext
                .Groups
                .Include(x => x.Students)
                .ToListAsync();

            var filter = new GroupFilter(SchoolDbContext.Groups, filterParameters);
            return await filter
                .ApplyFilter()
                
                .Include(g => g.Students)
                //.SelectMany(
                //    g => g.Students.DefaultIfEmpty(),
                //    (g, s) => new GroupWithStudentCount
                //    {
                //        Group = g,
                //        StudentCount = g.Students.Count
                //    }
                //)
                .Select(g => new GroupWithStudentCount
                {
                    Group = g,
                    StudentCount = g.Students.Count
                })
                .ToListAsync();
        }
    }
}
