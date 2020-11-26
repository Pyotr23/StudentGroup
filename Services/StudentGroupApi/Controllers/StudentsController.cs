using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudentGroup.Infrastracture.Data.Models;
using StudentGroup.Infrastracture.Shared.Dto;
using StudentGroup.Infrastracture.Shared.Managers;
using StudentGroup.Services.Api.Models;

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
        public async Task<ActionResult<IEnumerable<StudentWithGroups>>> GetAllStudents()
        {
            var studentsWithGroups = await _schoolManager.GetAllStudentsWithGroups();
            var studentsWithGroupNames = studentsWithGroups                
                .Select(s => new StudentWithGroupNames
                {
                    Id = s.Student.Id,
                    Surname = s.Student.Surname,
                    Name = s.Student.Name,
                    MiddleName = s.Student.MiddleName,
                    Nickname = s.Student.Nickname,
                    GroupNames = s.Groups == null 
                        ? string.Empty 
                        : string.Join(", ", s.Groups.Select(g => g?.Name))
                });
            return Ok(studentsWithGroupNames);
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
