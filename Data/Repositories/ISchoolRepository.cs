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

        /// <summary>
        ///     Обновление студента.
        /// </summary>
        /// <param name="student">Студент</param>
        /// <returns></returns>
        Task UpdateStudent(Student student);

        /// <summary>
        ///     Найти группу.
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Найденная группа или null.</returns>
        Task<Group> FindGroupAsync(int id);

        /// <summary>
        ///     Добавление группы.
        /// </summary>
        /// <param name="group">Группа</param>
        /// <returns>Добавленная группа.</returns>
        Task<Group> AddGroupAsync(Group group);

        /// <summary>
        ///     Удаление группы.
        /// </summary>
        /// <param name="group">Группа</param>
        /// <returns></returns>
        Task RemoveGroup(Group group);

        /// <summary>
        ///     Обновить группу.
        /// </summary>
        /// <param name="group">Группа</param>
        /// <returns></returns>
        Task UpdateGroup(Group group);

        /// <summary>
        ///     Добавить студента в группу.
        /// </summary>
        /// <param name="groupStudent">Связь "группа - студент"</param>
        /// <returns></returns>
        Task AddStudentToGroupAsync(GroupStudent groupStudent);

        /// <summary>
        ///     Найти запись в таблице связи "группа - студент".
        /// </summary>
        /// <param name="groupStudent">Сущность "группа - студент"</param>
        /// <returns>Найденная запись или null.</returns>
        Task<GroupStudent> FindGroupStudentAsync(GroupStudent groupStudent);

        /// <summary>
        ///     Удалить студента из группы.
        /// </summary>
        /// <param name="groupStudent">Связь "группа - студент"</param>
        /// <returns></returns>
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
