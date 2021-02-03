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
        
        public async Task<IEnumerable<Student>> GetStudentsAsync(StudentFilterParameters filterParameters)
        {
            StudentFilter studentFilter = new(SchoolDbContext.Students, filterParameters);
            return await studentFilter
                .ApplyFilter()
                .Include(s => s.Groups)
                .Where(student => string.IsNullOrEmpty(filterParameters.GroupName)
                    || student
                        .Groups
                        .Any(g => g.Name == filterParameters.GroupName)
                )               
                .OrderBy(s => s.Id)
                .Skip(filterParameters.SkipCount)
                .Take(filterParameters.PageSize == 0 
                    ? int.MaxValue
                    : filterParameters.PageSize)                
                .ToListAsync();
        }        

        public async Task<bool> IsUniqueNicknameAsync(string nickname)
        {
            return !await SchoolDbContext
                    .Students
                    .AnyAsync(s => s.Nickname == nickname);
        }
    }
}
