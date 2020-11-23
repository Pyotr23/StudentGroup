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

        public async Task<IEnumerable<StudentWithGroups>> GetStudentsWithGroupsAsync()
        {
            return await _context
                .Students                
                .Join(_context.GroupStudents, 
                    s => s.Id, 
                    gs => gs.StudentId,
                    (student, groupStudent) => new { student, groupStudent })
                .GroupJoin(_context.Groups,
                    gs => gs.groupStudent.GroupId,
                    g => g.Id,
                    (x, y) => new StudentWithGroups 
                    {
                        Student = x.student,
                        Groups = y
                    })
                .Select(x => x)
                .ToListAsync();            
        }

        public Student PostStudent(Student student)
        {
            throw new System.NotImplementedException();
        }
    }
}
