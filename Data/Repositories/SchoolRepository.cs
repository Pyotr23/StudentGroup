using Microsoft.EntityFrameworkCore;
using StudentGroup.Infrastracture.Data.Contexts;
using StudentGroup.Infrastracture.Data.Filters;
using StudentGroup.Infrastracture.Data.Models;
using StudentGroup.Infrastracture.Data.Models.Database;
using StudentGroup.Infrastracture.Data.Models.Filtration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentGroup.Infrastracture.Data.Repositories
{
    public class SchoolRepository : ISchoolRepository
    {
        private SchoolContext _context;

        public SchoolRepository(SchoolContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Group>> GetGroupsAsync()
        {
            return await _context
                .Groups
                .ToListAsync();
        }

        /// <summary>
        ///     Получить отфильтрованный список студентов с именем группы.
        /// </summary>
        /// <param name="filteringParameters">Параметры фильтрации</param>
        /// <returns>Отфильтрованный список студентов с именем группы</returns>
        public async Task<IEnumerable<StudentWithGroupName>> GetStudentsWithGroupNameAsync(
            FilteringParameters filteringParameters
            )
        {
            var studentFilter = new StudentFilter(_context.Students, filteringParameters.StudentFilteringParameters);                       

            var query = from student in studentFilter.ApplyFilter()
                        join groupStudent in _context.GroupStudents
                            on student.Id equals groupStudent.StudentId into grst

                        from gs in grst.DefaultIfEmpty()
                        join gr in _context.Groups
                            on gs.GroupId equals gr.Id into groups

                        from g in groups.DefaultIfEmpty()
                        select new StudentWithGroupName { Student = student, GroupName = g.Name };

            var studentWithGroupNameFilter = new StudentWithGroupNameFilter(query, filteringParameters.GroupFilteringParameters);
            query = studentWithGroupNameFilter.ApplyFilter();           

            query = filteringParameters.PageSize == null
                ? query
                : query.Take((int)filteringParameters.PageSize);

            return await query.ToArrayAsync();
        }        

        /// <summary>
        ///     Добавить нового студента.
        /// </summary>
        /// <param name="student">Добавляемый студент</param>
        /// <returns>Добавленный студент</returns>
        public async Task<Student> AddStudentAsync(Student student)
        {
            await _context
                .Students
                .AddAsync(student);
            await _context.SaveChangesAsync();
            return student;
        }   
        
        /// <summary>
        ///     Поиск студента по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Найденный студент или null</returns>
        public async Task<Student> FindStudentAsync(int id)
        {
            return await _context
                .Students
                .FindAsync(id);
        }

        /// <summary>
        ///     Удаление студента.
        /// </summary>
        /// <param name="student">Студент</param>
        /// <returns></returns>
        public async Task RemoveStudent(Student student)
        {
            _context
                .Students
                .Remove(student);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        ///     Обновление студента.
        /// </summary>
        /// <param name="student">Студент</param>
        /// <returns></returns>
        public async Task UpdateStudent(Student student)
        {
            _context
                .Students
                .Update(student);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        ///     Найти группу.
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Найденная группа или null.</returns>
        public async Task<Group> FindGroupAsync(int id)
        {
            return await _context
                .Groups
                .FindAsync(id);
        }

        /// <summary>
        ///     Добавление группы.
        /// </summary>
        /// <param name="group">Группа</param>
        /// <returns>Добавленная группа.</returns>
        public async Task<Group> AddGroupAsync(Group group)
        {
            await _context
                .Groups
                .AddAsync(group);
            await _context.SaveChangesAsync();
            return group;
        }

        /// <summary>
        ///     Удаление группы.
        /// </summary>
        /// <param name="group">Группа</param>
        /// <returns></returns>
        public async Task RemoveGroup(Group group)
        {
            _context
                .Groups
                .Remove(group);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        ///     Обновить группу.
        /// </summary>
        /// <param name="group">Группа</param>
        /// <returns></returns>
        public async Task UpdateGroup(Group group)
        {
            _context
                .Groups
                .Update(group);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        ///     Добавить студента в группу.
        /// </summary>
        /// <param name="groupStudent">Связь "группа - студент"</param>
        /// <returns></returns>
        public async Task AddStudentToGroupAsync(GroupStudent groupStudent)
        {
            await _context
                .GroupStudents
                .AddAsync(groupStudent);
            await _context.SaveChangesAsync();
        } 

        /// <summary>
        ///     Найти запись в таблице связи "группа - студент".
        /// </summary>
        /// <param name="groupStudent">Сущность "группа - студент"</param>
        /// <returns>Найденная запись или null.</returns>
        public async Task<GroupStudent> FindGroupStudentAsync(GroupStudent groupStudent)
        {
            return await _context
                .GroupStudents
                .FirstOrDefaultAsync(x => x.GroupId == groupStudent.GroupId && x.StudentId == groupStudent.StudentId);
        }

        /// <summary>
        ///     Удалить студента из группы.
        /// </summary>
        /// <param name="groupStudent">Связь "группа - студент"</param>
        /// <returns></returns>
        public async Task RemoveGroupStudent(GroupStudent groupStudent)
        {
            _context
                .GroupStudents
                .Remove(groupStudent);
            await _context.SaveChangesAsync();            
        }

        /// <summary>
        ///     Получить отфильтрованный по имени список групп.
        /// </summary>
        /// <param name="filteringParameters">Параметры фильтрации</param>
        /// <returns>Список групп из таблицы "Группы" с идентификатором студента.</returns>
        public async Task<IEnumerable<GroupWithStudentId>> GetGroupsAsync(GroupFilteringParameters filteringParameters)
        {
            var filter = new GroupFilter(_context.Groups, filteringParameters);
            var query = from gr in filter.ApplyFilter()
                        join groupStudent in _context.GroupStudents
                            on gr.Id equals groupStudent.GroupId into ggs
                        from gs in ggs.DefaultIfEmpty()
                        select new GroupWithStudentId
                        {
                            Id = gr.Id,
                            Name = gr.Name,
                            StudentId = gs == null
                                ? null
                                : (int?)gs.StudentId
                        };
            return await query.ToListAsync();            
        }
    }
}
