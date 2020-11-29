using StudentGroup.Infrastracture.Data.Models;
using StudentGroup.Infrastracture.Data.Models.Database;
using StudentGroup.Infrastracture.Data.Models.Filtration;
using StudentGroup.Infrastracture.Shared.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentGroup.Infrastracture.Shared.Managers
{
    public interface ISchoolManager
    {
        /// <summary>
        ///     Получение студента.
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Студент, если существует с таким идентификатором; в противном случае - null.</returns>
        Task<Student> GetStudent(int id);

        /// <summary>
        ///     Удаление студента.
        /// </summary>
        /// <param name="student">Студент</param>
        /// <returns></returns>
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

        /// <summary>
        ///     Получить список студентов после необязательной фильтрации.
        /// </summary>
        /// <param name="filteringParameters">Параметры фильтрации</param>
        /// <returns>Список студнтов с полями: ID, ФИО, уникальный идентификатор, список групп через запятую</returns>
        Task<IEnumerable<GetStudentsResponse>> GetAllStudents(FilteringParameters filteringParameters);

        /// <summary>
        ///     Добавить нового студента.
        /// </summary>
        /// <param name="studentDto">DTO студента</param>
        /// <returns>Студент</returns>
        Task<Student> PostStudent(StudentDto studentDto);
    }
}
