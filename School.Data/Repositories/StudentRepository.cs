using Microsoft.EntityFrameworkCore;
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
            var groupFilter = new GroupFilter(SchoolDbContext.Groups, filterParameters.GroupName);

            var query = from student in studentFilter.ApplyFilter()
                        join studentGroup in SchoolDbContext.StudentGroups
                            on student.Id equals studentGroup.StudentId into stgr

                        from gs in stgr.DefaultIfEmpty()
                        join gr in groupFilter.ApplyFilter()
                            on gs.GroupId equals gr.Id into groups

                        from g in groups.DefaultIfEmpty()
                        select new StudentWithGroupName { Student = student, GroupName = g.Name };            

            return await query.ToListAsync();
        }

        private SchoolDbContext SchoolDbContext => Context as SchoolDbContext;
    }
}
