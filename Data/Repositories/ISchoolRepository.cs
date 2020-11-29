using StudentGroup.Infrastracture.Data.Models;
using StudentGroup.Infrastracture.Data.Models.Database;
using StudentGroup.Infrastracture.Data.Models.Filtration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentGroup.Infrastracture.Data.Repositories
{
    public interface ISchoolRepository
    {
        /// <summary>
        ///     Добавить нового студента.
        /// </summary>
        /// <param name="student">Добавляемый студент</param>
        /// <returns>Добавленный студент</returns>
        Task<Student> AddStudentAsync(Student student);

        Task<IEnumerable<Group>> GetGroupsAsync();

        /// <summary>
        ///     Поиск студента по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Найденный студент или null</returns>
        Task<Student> FindStudentAsync(int id);

        /// <summary>
        ///     Удаление студента.
        /// </summary>
        /// <param name="student">Студент</param>
        /// <returns></returns>
        Task RemoveStudent(Student student);
        Task UpdateStudent(Student student);
        Task<Group> FindGroupAsync(int id);
        Task<Group> AddGroupAsync(Group group);
        Task RemoveGroup(Group group);
        Task UpdateGroup(Group group);
        Task AddStudentToGroupAsync(GroupStudent groupStudent);
        Task<GroupStudent> FindGroupStudentAsync(GroupStudent groupStudent);
        Task RemoveGroupStudent(GroupStudent groupStudent);        
        Task<IEnumerable<GroupWithStudentId>> GetAllGroupsAsync();
        Task<IEnumerable<GroupWithStudentId>> GetAllGroupsAsync(string whereCondition);

        /// <summary>
        ///     Получить отфильтрованный список студентов с именем группы.
        /// </summary>
        /// <param name="filteringParameters">Параметры фильтрации</param>
        /// <returns>Отфильтрованный список студентов с именем группы</returns>
        Task<IEnumerable<StudentWithGroupName>> GetStudentsWithGroupNameAsync(FilteringParameters filteringParameters);
    }
}
