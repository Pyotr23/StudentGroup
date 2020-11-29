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

        /// <summary>
        ///     Обновление студента.
        /// </summary>
        /// <param name="student">Студент</param>
        /// <returns></returns>
        Task UpdateStudent(Student student);

        /// <summary>
        ///     Поиск группы.
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Найденная группа или null.</returns>
        Task<Group> GetGroup(int id);

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
        /// <param name="groupId">Идентификатор группы</param>
        /// <param name="studentId">Идентификатор студента</param>
        /// <returns></returns>
        Task AddStudentToGroup(int groupId, int studentId);

        /// <summary>
        ///     Получить запись из таблицы "группа - студент".
        /// </summary>
        /// <param name="groupId">Идентификатор группы</param>
        /// <param name="studentId">Идентификатор студента</param>
        /// <returns>Найденная запись или null.</returns>
        Task<GroupStudent> GetGroupStudent(int groupId, int studentId);

        /// <summary>
        ///     Удалить студента из группы. 
        /// </summary>
        /// <param name="groupStudent">Связь "группа - студент"</param>
        /// <returns></returns>
        Task RemoveGroupStudent(GroupStudent groupStudent);

        /// <summary>
        ///     Получить список групп с количеством студентов в них.
        /// </summary>
        /// <param name="whereCondition">Условие фильтрации имени</param>
        /// <returns>Список групп.</returns>
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

        /// <summary>
        ///     Добавление группы.
        /// </summary>
        /// <param name="groupDto">DTO группы</param>
        /// <returns>Созданная группа.</returns>
        Task<Group> PostGroup(GroupDto groupDto);
    }
}
