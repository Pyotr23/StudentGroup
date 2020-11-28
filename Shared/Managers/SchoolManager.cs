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

        public async Task<Student> PostStudent(Student student)
        {
            return await _schoolRepository.AddStudentAsync(student);
        }

        public async Task<IEnumerable<StudentWithGroupsDto>> GetAllStudents()
        {
            var students = await _schoolRepository.GetAllStudentsWithGroupNameAsync();
            return students
                .GroupBy(x => x.Student)
                .Select(s => new StudentWithGroupsDto
                {
                    Id = s.Key.Id,
                    Surname = s.Key.Surname,
                    Name = s.Key.Name,
                    MiddleName = s.Key.MiddleName,
                    Nickname = s.Key.Nickname,                    
                    GroupNamesString = string.Join("; ", s.Select(z => z.GroupName))
                })
                .ToArray();
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

        public async Task<GroupStudent> GetGroupStudent(int groupId, int studentId)
        {
            var groupStudent = new GroupStudent
            {
                GroupId = groupId,
                StudentId = studentId
            };
            return await _schoolRepository.FindGroupStudentAsync(groupStudent);
        }

        public async Task RemoveGroupStudent(GroupStudent groupStudent)
        {
            await _schoolRepository.RemoveGroupStudent(groupStudent);
        }

        public async Task<IEnumerable<GroupWithStudentCount>> GetAllGroupsWithStudentCount(string whereCondition)
        {
            var groups =  string.IsNullOrEmpty(whereCondition)
                ? await _schoolRepository.GetAllGroupsAsync()
                : await _schoolRepository.GetAllGroupsAsync(whereCondition);
            return groups
                .GroupBy(x => new Group
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .Select(y => new GroupWithStudentCount
                {
                    Id = y.Key.Id,
                    Name = y.Key.Name,
                    StudentCount = y.Count(z => z.StudentId != null)
                });
        }
    }
}
