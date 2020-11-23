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
                .GroupJoin(_context.GroupStudents, 
                    s => s.Id, 
                    gs => gs.StudentId,
                    (student, groupStudents) => new { Student = student, Groups = groupStudents })
                .SelectMany(
                    xy => xy.Groups.DefaultIfEmpty(),
                    (x, y) => new { Student = x.Student, GroupStudent = y })
                .Select(s => new StudentWithGroups
                {
                    Student = s.Student,
                    Groups = new Group { Id = 1, Name = "" }
                })
                //.Join(_context.Groups,
                //    gs => gs.groupStudent.GroupId,
                //    g => g.Id,
                //    (x, y) => new StudentWithGroups 
                //    {
                //        Student = x.student,
                //        Groups = y
                //    })
                .ToListAsync();            
        }

        public Student PostStudent(Student student)
        {
            throw new System.NotImplementedException();
        }
    }
}
