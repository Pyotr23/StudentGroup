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
            var studentsWithGroupIds = await _context
                .Students
                .GroupJoin(_context.GroupStudents,
                    s => s.Id,
                    gs => gs.StudentId,
                    (student, groupStudents) => new StudentWithGroupStudents { Student = student, GroupStudents = groupStudents })
                .SelectMany(
                    xy => xy.GroupStudents.DefaultIfEmpty(),
                    (x, y) => new StudentWithGroupIds { Student = x.Student, GroupId = y == null ? null : (int?)y.GroupId })
                .ToListAsync();
            return studentsWithGroupIds

                .Select(f => new StudentWithGroups() { Student = f.Student })
                //.Join(_context.Groups,
                //    gs => gs.groupStudent.GroupId,
                //    g => g.Id,
                //    (x, y) => new StudentWithGroups 
                //    {
                //        Student = x.student,
                //        Groups = y
                //    })
                .ToArray();
        }

        public Student PostStudent(Student student)
        {
            throw new System.NotImplementedException();
        }
    }
}
