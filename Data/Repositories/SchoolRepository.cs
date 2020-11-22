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
                    (s, gs) => new  
                    {
                        Student = s,
                        gs.Group
                    })
                .GroupBy(x => x.Student)
                .Select(y => new StudentWithGroups
                {
                    Id = y.Key.Id,
                    Surname = y.Key.Surname,
                    Name = y.Key.Name,
                    MiddleName = y.Key.MiddleName,
                    Nickname = y.Key.Nickname,
                    Groups = y.Select(s => s.Group)
                })                
                .ToListAsync();            
        }

        public Student PostStudent(Student student)
        {
            throw new System.NotImplementedException();
        }
    }
}
