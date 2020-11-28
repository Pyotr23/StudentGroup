using StudentGroup.Infrastracture.Data.Models;
using StudentGroup.Infrastracture.Shared.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentGroup.Infrastracture.Shared.Managers
{
    public interface ISchoolManager
    {
        Task<Student> PostStudent(Student addStudentDto);        
        Task<IEnumerable<Group>> GetAllGroups();
        Task<Student> GetStudent(int id);
        Task RemoveStudent(Student student);
        Task UpdateStudent(Student student);
        Task<Group> GetGroup(int id);
        Task<Group> PostGroup(Group group);
        Task RemoveGroup(Group group);
        Task UpdateGroup(Group group);
        Task AddStudentToGroup(int groupId, int studentId);
        Task<GroupStudent> GetGroupStudent(int groupId, int studentId);
        Task RemoveGroupStudent(GroupStudent groupStudent);
        Task<IEnumerable<GroupWithStudentCount>> GetAllGroupsWithStudentCount(string whereCondition);
        Task<IEnumerable<StudentWithGroupsDto>> GetAllStudents();
    }
}
