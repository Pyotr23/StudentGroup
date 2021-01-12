using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using School.Api.Resources;
using School.Api.Validators;
using School.Core.DTOes;
using School.Core.Filtration.Parameters;
using School.Core.Models;
using School.Core.Services;

namespace School.Api.Controllers
{
    /// <summary>
    ///     Контроллер для управления студентами.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;

        /// <summary>
        ///     Конструктор контроллера Student.
        /// </summary>
        /// <param name="studentService"> Сервис для управления студентами. </param>
        /// <param name="mapper"> Интерфейс маппера. </param>
        public StudentsController(IStudentService studentService, IMapper mapper)
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
            if (id <= 0)
                return BadRequest();

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
            var validator = new SaveStudentResourceValidator();
            var validationResult = await validator.ValidateAsync(saveStudentResource);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var nickname = saveStudentResource.Nickname;
            var isNullOrUniqueNickname = string.IsNullOrEmpty(nickname)
                || await _studentService.IsUniqueNickname(nickname);
            if (!isNullOrUniqueNickname)
                return BadRequest("Nickname должно быть пустым или уникальным.");

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
            var validator = new SaveStudentResourceValidator();
            var validationResult = await validator.ValidateAsync(saveStudentResource);
            var isValidRequest = id > 0 && validationResult.IsValid;
            if (!isValidRequest)
                return BadRequest();

            if (!await _studentService.IsUniqueNickname(saveStudentResource.Nickname, id))
                return BadRequest("Nickname должно быть пустым или уникальным.");

            var studentForUpdate = await _studentService.GetStudentById(id);
            if (studentForUpdate == null)
                return NotFound();

            var student = _mapper.Map<Student>(saveStudentResource);
            await _studentService.UpdateStudent(studentForUpdate, student);

            var updatedStudent = await _studentService.GetWithGroupNames(id);
            var updatedStudentResource = _mapper.Map<StudentResource>(updatedStudent);
            return Ok(updatedStudentResource);
        }

        /// <summary>
        ///     Удалить студента.
        /// </summary>
        /// <param name="id"> Идентификатор студента. </param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            if (id <= 0)
                return BadRequest();

            var student = await _studentService.GetStudentById(id);
            if (student == null)
                return NotFound();

            await _studentService.DeleteStudent(student);
            return NoContent();
        }

        /// <summary>
        ///     Получить студентов с возможностью фильтрации. 
        /// </summary>
        /// <param name="filterParameters"> Параметры фильтрации. </param>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentResource>>> GetStudents(
            [FromQuery] StudentFilterParameters filterParameters)
        {
            var studentDtoes = await _studentService.GetAllWithGroupNames(filterParameters);
            var studentResources = studentDtoes
                .Select(dto => _mapper.Map<StudentResource>(dto))
                .ToList();
            return Ok(studentResources);
        }
    }
}
