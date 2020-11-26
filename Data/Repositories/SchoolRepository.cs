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

        public async Task<Student> AddStudentAsync(Student student)
        {
            await _context
                .Students
                .AddAsync(student);
            await _context.SaveChangesAsync();
            return student;
        }   
        
        public async Task<Student> FindStudentAsync(int id)
        {
            return await _context
                .Students
                .FindAsync(id);
        }

        public async Task RemoveStudent(Student student)
        {
            _context
                .Students
                .Remove(student);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateStudent(Student student)
        {
            _context
                .Students
                .Update(student);
            await _context.SaveChangesAsync();
        }

        public async Task<Group> FindGroupAsync(int id)
        {
            return await _context
                .Groups
                .FindAsync(id);
        }

        public async Task<Group> AddGroupAsync(Group group)
        {
            await _context
                .Groups
                .AddAsync(group);
            await _context.SaveChangesAsync();
            return group;
        }

        public async Task RemoveGroup(Group group)
        {
            _context
                .Groups
                .Remove(group);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateGroup(Group group)
        {
            _context
                .Groups
                .Update(group);
            await _context.SaveChangesAsync();
        }

        public async Task AddStudentToGroupAsync(GroupStudent groupStudent)
        {
            await _context
                .GroupStudents
                .AddAsync(groupStudent);
            await _context.SaveChangesAsync();
        } 
    }
}
