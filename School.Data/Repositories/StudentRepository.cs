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
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        private SchoolDbContext SchoolDbContext => Context as SchoolDbContext;

        public StudentRepository(SchoolDbContext context) : base(context)
        { }

        public async Task<Student> GetByIdAsync(int id)
        {
            return await SchoolDbContext
                .Students
                .FindAsync(id);
        }

        public async Task<IEnumerable<StudentWithGroupName>> GetStudentsWithGroupNameAsync(StudentFilterParameters filterParameters)
        {
            var studentFilter = new StudentFilter(SchoolDbContext.Students, filterParameters);

            IQueryable<StudentWithGroupName> query;

            if (string.IsNullOrEmpty(filterParameters.GroupName))
            {
                query = from student in studentFilter.ApplyFilter()
                        join studentGroup in SchoolDbContext.StudentGroups
                            on student.Id equals studentGroup.StudentId into stgr

                        from gs in stgr.DefaultIfEmpty()
                        join gr in SchoolDbContext.Groups
                            on gs.GroupId equals gr.Id into groups

                        from g in groups.DefaultIfEmpty()
                        select new StudentWithGroupName { Student = student, GroupName = g.Name };
            }
            else
            {
                var groupFilter = new GroupFilter(SchoolDbContext.Groups, filterParameters.GroupName);

                query = from student in studentFilter.ApplyFilter()
                            join studentGroup in SchoolDbContext.StudentGroups
                                on student.Id equals studentGroup.StudentId into stgr

                            from gs in stgr.DefaultIfEmpty()
                            join gr in groupFilter.ApplyFilter()
                                on gs.GroupId equals gr.Id into groups

                            from g in groups
                            select new StudentWithGroupName { Student = student, GroupName = g.Name };
            }

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<StudentWithGroupName>> GetStudentsWithGroupNameAsync(int id)
        {
            var query = from student in SchoolDbContext.Students
                        where student.Id == id
                        join studentGroup in SchoolDbContext.StudentGroups
                            on student.Id equals studentGroup.StudentId into stgr

                        from gs in stgr.DefaultIfEmpty()
                        join gr in SchoolDbContext.Groups
                            on gs.GroupId equals gr.Id into groups

                        from g in groups.DefaultIfEmpty()
                        select new StudentWithGroupName { Student = student, GroupName = g.Name };
            return await query.ToListAsync();
        }

        public async Task<bool> IsUniqueNickname(string nickname)
        {
            return !await SchoolDbContext
                    .Students
                    .AnyAsync(s => s.Nickname == nickname);
        }

        public async Task<int?> GetIdByNickname(string nickname)
        {
            var student = await SchoolDbContext
                .Students
                .FirstOrDefaultAsync(s => s.Nickname == nickname);
            return student == null
                ? null
                : student.Id;
        }
    }
}
