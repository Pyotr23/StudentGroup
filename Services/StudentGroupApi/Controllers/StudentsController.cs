using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudentGroup.Infrastracture.Data.Models;
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentWithGroups>>> GetStudents(
            [FromQuery] string sex,
            [FromQuery] string surname,
            [FromQuery] string name,
            [FromQuery] string middleName,
            [FromQuery] string nickname
            )
        {
            var students = await _schoolManager.GetAllStudents(sex, surname, name, middleName, nickname);            
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
