using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using School.Api.Resources;
using School.Core.DTOes;
using School.Core.Models;
using School.Core.Services;

namespace School.Api.Controllers
{
    /// <summary>
    ///     Контроллер для управления студентами.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;

        /// <summary>
        ///     Конструктор контроллера Student.
        /// </summary>
        /// <param name="studentService"> Сервис для управления студентами. </param>
        /// <param name="mapper"> Интерфейс маппера. </param>
        public StudentController(IStudentService studentService, IMapper mapper)
        {
            _studentService = studentService;
            _mapper = mapper;
        }

        /// <summary>
        ///     Получить студента по идентификатору.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentResource>> GetStudentById(int id)
        {
            //var student = await _studentService.GetStudentById(id);
            //if (student == null)
            //    return NotFound();

            //var studentResource = _mapper.Map<Student, StudentResource>(student);
            //return Ok(studentResource);

            var studentDto = await _studentService.GetWithGroupNames(id);
            if (studentDto == null)
                return NotFound();
            var studentResource = _mapper.Map<StudentWithGroupsDto, StudentResource>(studentDto);
            return Ok(studentResource);
        }

        /// <summary>
        ///     Создать студента.
        /// </summary>
        /// <param name="saveStudentResource"> Создаваемый студент. </param>
        [HttpPost]
        public async Task<ActionResult<StudentResource>> CreateStudent([FromBody] SaveStudentResource saveStudentResource)
        {
            var studentToCreate = _mapper.Map<Student>(saveStudentResource);
            var newStudent = await _studentService.CreateStudent(studentToCreate);
            var student = await _studentService.GetStudentById(newStudent.Id);
            var studentResource = _mapper.Map<StudentResource>(student);
            return Ok(studentResource);
        }

        /// <summary>
        ///     Обновить информацию студента.
        /// </summary>
        /// <param name="id"> Идентификатор обновляемого студента. </param>
        /// <param name="saveStudentResource"> Новое описание студента. </param>
        [HttpPut("{id}")]
        public async Task<ActionResult<StudentResource>> UpdateStudent(int id, 
            [FromBody] SaveStudentResource saveStudentResource)
        {
            var isValidRequest = id > 0;
            if (!isValidRequest)
                return BadRequest();

            var studentForUpdate = await _studentService.GetStudentById(id);
            if (studentForUpdate == null)
                return NotFound();

            var student = _mapper.Map<Student>(saveStudentResource);
            await _studentService.UpdateStudent(studentForUpdate, student);

            var updatedStudent = await _studentService.GetStudentById(id);
            var updatedStudentResource = _mapper.Map<StudentResource>(updatedStudent);
            return Ok(updatedStudentResource);
        }
    }
}
