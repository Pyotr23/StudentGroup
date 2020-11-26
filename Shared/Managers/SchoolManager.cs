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

        public async Task<Student> PostStudent(Student student)
        {
            return await _schoolRepository.AddStudentAsync(student);
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
            return await _schoolRepository.FindStudentAsync(id);
        }

        public async Task RemoveStudent(Student student)
        {
            await _schoolRepository.RemoveStudent(student);
        }

        public async Task UpdateStudent(Student student)
        {
            await _schoolRepository.UpdateStudent(student);
        }

        public async Task<Group> GetGroup(int id)
        {
            return await _schoolRepository.FindGroupAsync(id);
        }

        public async Task<Group> PostGroup(Group group)
        {
            return await _schoolRepository.AddGroupAsync(group);
        }

        public async Task RemoveGroup(Group group)
        {
            await _schoolRepository.RemoveGroup(group);
        }

        public async Task UpdateGroup(Group group)
        {
            await _schoolRepository.UpdateGroup(group);
        }

        public async Task AddStudentToGroup(int groupId, int studentId)
        {
            var groupStudent = new GroupStudent
            {
                GroupId = groupId,
                StudentId = studentId
            };
            await _schoolRepository.AddStudentToGroupAsync(groupStudent);
        }
    }
}
