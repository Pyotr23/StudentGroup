using Microsoft.EntityFrameworkCore;
using StudentGroup.Infrastracture.Data.Builders;
using StudentGroup.Infrastracture.Data.Contexts;
using StudentGroup.Infrastracture.Data.Models;
using StudentGroup.Infrastracture.Data.Models.Database;
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

        public async Task<IEnumerable<StudentWithGroupName>> GetStudentsWithGroupNameAsync(
            string sex,
            string surname,
            string name,
            string middleName,
            string nickname
            )
        {
            var studentQueryBuilder = new StudentQueryBuilder(_context.Students);

            if (!string.IsNullOrEmpty(sex))
                studentQueryBuilder.WithSexCondition(sex);
            if (!string.IsNullOrEmpty(surname))
                studentQueryBuilder.WithSurnameCondition(surname);
            if (!string.IsNullOrEmpty(name))
                studentQueryBuilder.WithNameCondition(name);
            if (!string.IsNullOrEmpty(middleName))
                studentQueryBuilder.WithSexCondition(middleName);
            if (!string.IsNullOrEmpty(nickname))
                studentQueryBuilder.WithSexCondition(nickname);

            var query = from student in studentQueryBuilder.Query                       
                        join groupStudent in _context.GroupStudents
                            on student.Id equals groupStudent.StudentId into grst

                        from gs in grst.DefaultIfEmpty()
                        join gr in _context.Groups
                            on gs.GroupId equals gr.Id into groups

                        from g in groups.DefaultIfEmpty()
                        select new StudentWithGroupName { Student = student, GroupName = g.Name };

            return await query.ToArrayAsync();            
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

        public async Task<GroupStudent> FindGroupStudentAsync(GroupStudent groupStudent)
        {
            return await _context
                .GroupStudents
                .FirstOrDefaultAsync(x => x.GroupId == groupStudent.GroupId && x.StudentId == groupStudent.StudentId);
        }

        public async Task RemoveGroupStudent(GroupStudent groupStudent)
        {
            _context
                .GroupStudents
                .Remove(groupStudent);
            await _context.SaveChangesAsync();            
        }

        public async Task<IEnumerable<GroupWithStudentId>> GetAllGroupsAsync()
        {
            var query = from gr in _context.Groups
                        join groupStudent in _context.GroupStudents
                            on gr.Id equals groupStudent.GroupId into ggs
                        from gs in ggs.DefaultIfEmpty()
                        select new GroupWithStudentId
                        {
                            Id = gr.Id,
                            Name = gr.Name,
                            StudentId = gs == null
                                ? null
                                : (int?)gs.StudentId
                        };
            return await query.ToListAsync();            
        }

        public async Task<IEnumerable<GroupWithStudentId>> GetAllGroupsAsync(string whereCondition)
        {
            var query = from gr in _context.Groups
                        where gr.Name == whereCondition
                        join groupStudent in _context.GroupStudents
                            on gr.Id equals groupStudent.GroupId into ggs
                        from gs in ggs.DefaultIfEmpty()
                        select new GroupWithStudentId 
                        { 
                            Id = gr.Id, 
                            Name = gr.Name, 
                            StudentId = gs == null 
                                ? null
                                : (int?)gs.StudentId 
                        };
            return await query.ToListAsync();
        }                
    }
}
