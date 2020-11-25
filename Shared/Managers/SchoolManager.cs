using StudentGroup.Infrastracture.Data.Models;
using StudentGroup.Infrastracture.Data.Repositories;
using StudentGroup.Infrastracture.Shared.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentGroup.Infrastracture.Shared.Managers
{
    public class SchoolManager : ISchoolManager
    {
        private readonly ISchoolRepository _schoolRepository;

        public SchoolManager(ISchoolRepository schoolRepository)
        {
            _schoolRepository = schoolRepository;
        }

        public async Task<IEnumerable<StudentWithGroups>> GetAllStudentsWithGroups()
        {
            var studentWithGroupIds = await GetAllStudentsWithGroupIds();
            var groups = await GetAllGroups();
            var studentsWithoutGroups = studentWithGroupIds
                .Where(s => s.GroupId == null)
                .Select(s => new StudentWithGroups { Student = s.Student, Groups = null });
            var studentWithGroups = studentWithGroupIds
                .Where(s => s.GroupId != null)
                .Join(groups,
                    s => s.GroupId,
                    g => g.Id,
                    (swgi, g) => new { swgi.Student, Group = g })
                .GroupBy(x => x.Student)
                .Select(y => new StudentWithGroups { Student = y.Key, Groups = y.Select(z => z.Group) });                
            return studentsWithoutGroups
                .Union(studentWithGroups)
                .ToList();                                    
        }

        public async Task<Student> PostStudent(Student addStudentDto)
        {
            return await _schoolRepository.AddStudentAsync(addStudentDto);
        }

        public async Task<IEnumerable<StudentWithGroupIds>> GetAllStudentsWithGroupIds()
        {
            return await _schoolRepository.GetStudentsWithGroupIdsAsync();
        }

        public async Task<IEnumerable<Group>> GetAllGroups()
        {
            return await _schoolRepository.GetGroupsAsync();
        }

        public async Task<Student> GetStudent(int id)
        {
            return await _schoolRepository.FindAsync(id);
        }
    }
}
