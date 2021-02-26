using Microsoft.EntityFrameworkCore;
using School.Core.Filtration.Filters;
using School.Core.Filtration.Parameters;
using School.Core.Models;
using School.Core.Repositories;
using School.Data.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace School.Data.Repositories
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        private SchoolDbContext SchoolDbContext => Context as SchoolDbContext;

        public StudentRepository(SchoolDbContext context) : base(context)
        { }
        
        public async Task<IEnumerable<Student>> GetStudentsAsync(StudentFilterParameters filterParameters)
        {
            StudentFilter studentFilter = new(SchoolDbContext.Students, filterParameters);
            return await studentFilter
                .ApplyFilter()
                .Include(s => s.Groups)
                .FilterByGroupName(filterParameters.GroupName)           
                .WithPagination(filterParameters)                                
                .ToListAsync();
        }        

        public async Task<bool> IsUniqueNicknameAsync(string nickname)
        {
            return !await SchoolDbContext
                    .Students
                    .AnyAsync(s => s.Nickname == nickname);
        }

        public async Task<Student> GetStudentWithGroupsByIdAsync(int id)
        {
            return await SchoolDbContext
                .Students
                .Include(s => s.Groups)
                .FirstOrDefaultAsync(s => s.Id == id);
        }
    }
}
