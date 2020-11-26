﻿using StudentGroup.Infrastracture.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentGroup.Infrastracture.Data.Repositories
{
    public interface ISchoolRepository
    {
        Task<Student> AddStudentAsync(Student student);
        Task<IEnumerable<StudentWithGroupIds>> GetStudentsWithGroupIdsAsync();
        Task<IEnumerable<Group>> GetGroupsAsync();
        Task<Student> FindStudentAsync(int id);
        Task RemoveStudent(Student student);
        Task UpdateStudent(Student student);
        Task<Group> FindGroupAsync(int id);
        Task<Group> AddGroupAsync(Group group);
        Task RemoveGroup(Group group);
        Task UpdateGroup(Group group);
        Task AddStudentToGroupAsync(GroupStudent groupStudent);
    }
}
