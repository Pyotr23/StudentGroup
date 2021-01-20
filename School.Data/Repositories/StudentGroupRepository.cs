using Microsoft.EntityFrameworkCore;
using School.Core.Models;
using School.Core.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace School.Data.Repositories
{
    public class StudentGroupRepository : Repository<StudentGroup>, IStudentGroupRepository
    {
        private SchoolDbContext SchoolDbContext => Context as SchoolDbContext;

        public StudentGroupRepository(SchoolDbContext context) : base(context)
        { }

        public async Task<StudentGroup> GetByIdes(int studentId, int groupId)
        {
            return await SchoolDbContext
                .StudentGroups
                .Where(sg => sg.StudentId == studentId)
                .FirstOrDefaultAsync(sg => sg.GroupId == groupId);
        }
    }
}
