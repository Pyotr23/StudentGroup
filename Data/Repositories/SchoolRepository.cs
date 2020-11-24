using Microsoft.EntityFrameworkCore;
using StudentGroup.Infrastracture.Data.Contexts;
using StudentGroup.Infrastracture.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentGroup.Infrastracture.Data.Repositories
{
    public class SchoolRepository : ISchoolRepository
    {
        private SchoolContext _context;

        public SchoolRepository(SchoolContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Group>> GetGroupsAsync()
        {
            return await _context
                .Groups
                .ToListAsync();
        }

        public async Task<IEnumerable<StudentWithGroupIds>> GetStudentsWithGroupIdsAsync()
        {
            return await _context
                .Students
                .GroupJoin(_context.GroupStudents,
                    s => s.Id,
                    gs => gs.StudentId,
                    (student, groupStudents) => new StudentWithGroupStudents { Student = student, GroupStudents = groupStudents })
                .SelectMany(
                    swgs => swgs.GroupStudents.DefaultIfEmpty(),
                    (swgs, gs) => new StudentWithGroupIds { Student = swgs.Student, GroupId = gs == null ? null : (int?)gs.GroupId })
                .ToListAsync();
        }

        public Student PostStudent(Student student)
        {
            throw new System.NotImplementedException();
        }
    }
}
