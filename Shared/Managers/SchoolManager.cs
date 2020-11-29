using StudentGroup.Infrastracture.Data.Models;
using StudentGroup.Infrastracture.Data.Models.Database;
using StudentGroup.Infrastracture.Data.Models.Filtration;
using StudentGroup.Infrastracture.Data.Repositories;
using StudentGroup.Infrastracture.Shared.Dto;
using StudentGroup.Infrastracture.Shared.Extensions;
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

        /// <summary>
        ///     Добавить нового студента.
        /// </summary>
        /// <param name="studentDto">DTO студента</param>
        /// <returns>Студент</returns>
        public async Task<Student> PostStudent(StudentDto studentDto)
        {
            return await _schoolRepository.AddStudentAsync(studentDto.ToStudent());
        }

        /// <summary>
        ///     Получить список студентов после необязательной фильтрации.
        /// </summary>
        /// <param name="filteringParameters">Параметры фильтрации</param>
        /// <returns>Список студнтов с полями: ID, ФИО, уникальный идентификатор, список групп через запятую</returns>
        public async Task<IEnumerable<GetStudentsResponse>> GetAllStudents(FilteringParameters filteringParameters)
        {
            var students = await _schoolRepository.GetStudentsWithGroupNameAsync(filteringParameters);
            return students
                .GroupBy(x => x.Student)
                .Select(s => new GetStudentsResponse
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

        /// <summary>
        ///     Получение студента по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Студент, если существует с таким идентификатором; в противном случае - null.</returns>
        public async Task<Student> GetStudent(int id)
        {
            return await _schoolRepository.FindStudentAsync(id);
        }

        /// <summary>
        ///     Удаление студента.
        /// </summary>
        /// <param name="student">Студент</param>
        /// <returns></returns>
        public async Task RemoveStudent(Student student)
        {
            await _schoolRepository.RemoveStudent(student);
        }

        /// <summary>
        ///     Обновление студента.
        /// </summary>
        /// <param name="student">Студент</param>
        /// <returns></returns>
        public async Task UpdateStudent(Student student)
        {
            await _schoolRepository.UpdateStudent(student);
        }

        /// <summary>
        ///     Поиск группы.
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Найденная группа или null.</returns>
        public async Task<Group> GetGroup(int id)
        {
            return await _schoolRepository.FindGroupAsync(id);
        }

        /// <summary>
        ///     Добавление группы.
        /// </summary>
        /// <param name="groupDto">DTO группы</param>
        /// <returns>Созданная группа.</returns>
        public async Task<Group> PostGroup(GroupDto groupDto)
        {
            return await _schoolRepository.AddGroupAsync(groupDto.ToGroup());
        }

        /// <summary>
        ///     Удаление группы.
        /// </summary>
        /// <param name="group">Группа</param>
        /// <returns></returns>
        public async Task RemoveGroup(Group group)
        {
            await _schoolRepository.RemoveGroup(group);
        }

        /// <summary>
        ///     Обновить группу.
        /// </summary>
        /// <param name="group">Группа</param>
        /// <returns></returns>
        public async Task UpdateGroup(Group group)
        {
            await _schoolRepository.UpdateGroup(group);
        }

        /// <summary>
        ///     Добавить студента в группу.
        /// </summary>
        /// <param name="groupId">Идентификатор группы</param>
        /// <param name="studentId">Идентификатор студента</param>
        /// <returns></returns>
        public async Task AddStudentToGroup(int groupId, int studentId)
        {
            var groupStudent = new GroupStudent
            {
                GroupId = groupId,
                StudentId = studentId
            };
            await _schoolRepository.AddStudentToGroupAsync(groupStudent);
        }

        /// <summary>
        ///     Получить запись из таблицы "группа - студент".
        /// </summary>
        /// <param name="groupId">Идентификатор группы</param>
        /// <param name="studentId">Идентификатор студента</param>
        /// <returns>Найденная запись или null.</returns>
        public async Task<GroupStudent> GetGroupStudent(int groupId, int studentId)
        {
            var groupStudent = new GroupStudent
            {
                GroupId = groupId,
                StudentId = studentId
            };
            return await _schoolRepository.FindGroupStudentAsync(groupStudent);
        }

        /// <summary>
        ///     Удалить студента из группы. 
        /// </summary>
        /// <param name="groupStudent">Связь "группа - студент"</param>
        /// <returns></returns>
        public async Task RemoveGroupStudent(GroupStudent groupStudent)
        {
            await _schoolRepository.RemoveGroupStudent(groupStudent);
        }

        /// <summary>
        ///     Получить список групп с количеством студентов в них.
        /// </summary>
        /// <param name="whereCondition">Условие фильтрации имени</param>
        /// <returns>Список групп.</returns>
        public async Task<IEnumerable<GroupWithStudentCount>> GetAllGroupsWithStudentCount(GroupFilteringParameters filteringParameters)
        {                        
            var groups =  await _schoolRepository.GetGroupsAsync(filteringParameters);
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
