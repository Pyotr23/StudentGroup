using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudentGroup.Infrastracture.Data.Models;
using StudentGroup.Infrastracture.Data.Models.Database;
using StudentGroup.Infrastracture.Data.Models.Filtration;
using StudentGroup.Infrastracture.Shared.Dto;
using StudentGroup.Infrastracture.Shared.Managers;

namespace StudentGroup.Services.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private ISchoolManager _schoolManager;

        public StudentsController(ISchoolManager schoolManager)
        {
            _schoolManager = schoolManager;
        }

        /// <summary>
        ///     Выборка студентов с фильтрацией
        /// </summary>
        /// <param name="sex">Текст фильтрации по полу студента</param>
        /// <param name="surname">Текст фильтрации по фамилии студента</param>
        /// <param name="name">Текст фильтрации по имени студента</param>
        /// <param name="middleName">Текст фильтрации по отчеству студента</param>
        /// <param name="nickname">Текст фильтрации по прозвищу студента</param>
        /// <param name="groupName">Текст фильтрации по названию группы</param>
        /// <param name="pageSize">Ограничение по количеству студентов</param>
        /// <returns>Результат выполнения запроса со списком, где каждый элемент содержит поля: 
        /// ID, ФИО, уникальный идентификатор, список групп через запятую.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetStudentsResponse>>> GetStudents(
            [FromQuery] string sex,
            [FromQuery] string surname,
            [FromQuery] string name,
            [FromQuery] string middleName,
            [FromQuery] string nickname,
            [FromQuery] string groupName,
            [FromQuery] int? pageSize
            )
        {
            var filteringParameters = new FilteringParameters
            {
                StudentFilteringParameters = new StudentFilteringParameters
                {
                    Sex = sex,
                    Surname = surname,
                    Name = name,
                    MiddleName = middleName,
                    Nickname = nickname
                },
                GroupFilteringParameters = new GroupFilteringParameters
                {
                    Name = groupName
                },
                PageSize = pageSize
            };
            var students = await _schoolManager.GetAllStudents(filteringParameters);            
            return Ok(students);
        }

        [HttpPost]
        public async Task<ActionResult<Student>> Post([FromBody] Student student)
        {  
            var newStudent = await _schoolManager.PostStudent(student);
            return CreatedAtAction("Get", new { id = newStudent.Id }, newStudent);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var student = await _schoolManager.GetStudent(id);
            if (student == null)
                return NotFound();
            return Ok(student);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Student>> Delete(int id)
        {
            var student = await _schoolManager.GetStudent(id);
            if (student == null)
                return NotFound();
            await _schoolManager.RemoveStudent(student);
            return Ok(student);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] StudentDto studentDto)
        {
            var student = await _schoolManager.GetStudent(id);
            if (student == null)
                return NotFound();
            student.MiddleName = studentDto.MiddleName;
            student.Name = studentDto.Name;
            student.Nickname = studentDto.Nickname;
            student.Sex = studentDto.Sex;
            student.Surname = studentDto.Surname;
            await _schoolManager.UpdateStudent(student);
            return NoContent();
        }
    }
}
