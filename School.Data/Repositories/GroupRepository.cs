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

        public async Task<GroupWithStudentCount> GetGroupWithStudentCountAsync(int id)
        {
            //return await SchoolDbContext
            //    .Groups
            //    .GroupJoin(SchoolDbContext.StudentGroups,
            //        g => g,
            //        sg => sg.Group,                    
            //        (g, sges) => new  
            //        {
            //            Group = g,
            //            StudentGroups = sges
            //        })                   
            //    .SelectMany(gsges => gsges.StudentGroups.DefaultIfEmpty(),
            //        (gsges, sg) => new  
            //        {
            //            Id = gsges.Group.Id,
            //            Name = gsges.Group.Name,
            //            StudentId = sg == null 
            //                ? (int?)null 
            //                : sg.StudentId
            //        })     
            //    .GroupBy(x => new Group 
            //    {
            //        Id = x.Id,
            //        Name = x.Name
            //    })
            //    .Select(grouped => new GroupWithStudentCount 
            //    { 
            //        Id = grouped.Key.Id,
            //        Name = grouped.Key.Name,
            //        StudentCount = grouped.Count(g => g.StudentId != null)
            //    })
            //    .FirstOrDefaultAsync(g => g.Id == id);                   
            return null;
        }

        public async Task<IEnumerable<GroupWithStudentCount>> GetAllGroups(GroupFilterParameters filterParameters)
        {
            //var filter = new GroupFilter(SchoolDbContext.Groups, filterParameters);
            //return await filter.ApplyFilter()
            //    .GroupJoin(SchoolDbContext.StudentGroups,
            //        g => g,
            //        sg => sg.Group,
            //        (g, sges) => new 
            //        {
            //            Group = g,
            //            StudentGroups = sges
            //        })
            //    .SelectMany(gsges => gsges.StudentGroups.DefaultIfEmpty(),
            //        (gsges, sg) => new 
            //        {
            //            Id = gsges.Group.Id,
            //            Name = gsges.Group.Name,
            //            StudentId = sg == null 
            //                ? (int?)null 
            //                : sg.StudentId
            //        })
            //    .GroupBy(x => new Group
            //    {
            //        Id = x.Id,
            //        Name = x.Name
            //    })
            //    .Select(grouped => new GroupWithStudentCount
            //    {
            //        Id = grouped.Key.Id,
            //        Name = grouped.Key.Name,
            //        StudentCount = grouped.Count(g => g.StudentId != null)
            //    })
            //    .ToListAsync();
            return null;
        }
    }
}
