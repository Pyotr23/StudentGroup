using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using School.Api.Resources;
using School.Api.Validators;
using School.Core.DTOes;
using School.Core.Filtration.Parameters;
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
        private readonly IStudentsService _studentService;
        private readonly IMapper _mapper;

        /// <summary>
        ///     Конструктор контроллера Student.
        /// </summary>
        /// <param name="studentService"> Сервис для управления студентами. </param>
        /// <param name="mapper"> Интерфейс маппера. </param>
        public StudentsController(IStudentsService studentService, IMapper mapper)
        {
            _studentService = studentService;
            _mapper = mapper;
        }

        /// <summary>
        ///     Получить список всех студентов.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentResource>>> GetAllStudents()
        {
            var dtoes = await _studentService.GetAllStudentsAsync();
            var resources = dtoes
                .Select(dto => _mapper.Map<StudentResource>(dto))
                .ToList();
            return Ok(resources);
        }

        /// <summary>
        ///     Получить студента по идентификатору.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        [HttpGet("{id}")]
        public async Task<ActionResult<FullStudentResource>> GetStudentById(int id)
        {
            if (id <= 0)
                return BadRequest();

            var studentDto = await _studentService.GetWithGroupNamesAsync(id);
            if (studentDto == null)
                return NotFound();

            var studentResource = _mapper.Map<FullStudentDto, FullStudentResource>(studentDto);
            return Ok(studentResource);
        }

        /// <summary>
        ///     Создать студента.
        /// </summary>
        /// <param name="saveStudentResource"> Создаваемый студент. </param>
        [HttpPost]
        public async Task<ActionResult<FullStudentResource>> CreateStudent([FromBody] SaveStudentResource saveStudentResource)
        {
            var validator = new SaveStudentResourceValidator();
            var validationResult = await validator.ValidateAsync(saveStudentResource);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var nickname = saveStudentResource.Nickname;
            var isNullOrUniqueNickname = string.IsNullOrEmpty(nickname)
                || await _studentService.IsUniqueNicknameAsync(nickname);
            if (!isNullOrUniqueNickname)
                return BadRequest("Nickname должно быть пустым или уникальным.");

            var studentDtoToCreate = _mapper.Map<StudentDto>(saveStudentResource);
            var newStudentDto = await _studentService.CreateStudentAsync(studentDtoToCreate);
            var createdStudentDto = await _studentService.GetStudentByIdAsync(newStudentDto.Id);
            var studentResource = _mapper.Map<FullStudentResource>(createdStudentDto);
            return Ok(studentResource);
        }

        /// <summary>
        ///     Обновить информацию студента.
        /// </summary>
        /// <param name="id"> Идентификатор обновляемого студента. </param>
        /// <param name="saveStudentResource"> Новое описание студента. </param>
        [HttpPut("{id}")]
        public async Task<ActionResult<FullStudentResource>> UpdateStudent(int id, 
            [FromBody] SaveStudentResource saveStudentResource)
        {
            var validator = new SaveStudentResourceValidator();
            var validationResult = await validator.ValidateAsync(saveStudentResource);
            var isValidRequest = id > 0 && validationResult.IsValid;
            if (!isValidRequest)
                return BadRequest();

            if (!await _studentService.IsUniqueNicknameAsync(saveStudentResource.Nickname, id))
                return BadRequest("Nickname должно быть пустым или уникальным.");

            var studentDto = _mapper.Map<StudentDto>(saveStudentResource);
            await _studentService.UpdateStudentAsync(id, studentDto);

            var updatedStudent = await _studentService.GetWithGroupNamesAsync(id);
            if (updatedStudent == null)
                return NotFound();

            var updatedStudentResource = _mapper.Map<FullStudentResource>(updatedStudent);
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

            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null)
                return NotFound();

            await _studentService.DeleteStudentAsync(student);
            return NoContent();
        }

        /// <summary>
        ///     Получить студентов с возможностью фильтрации. 
        /// </summary>
        /// <param name="filterParameters"> Параметры фильтрации. </param>
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<FullStudentResource>>> GetStudents(
        //    [FromQuery] StudentFilterParameters filterParameters)
        //{
        //    var studentDtoes = await _studentService.GetStudentsWithGroupNamesAsync(filterParameters);
        //    var studentResources = studentDtoes
        //        .Select(dto => _mapper.Map<FullStudentResource>(dto))
        //        .ToList();
        //    return Ok(studentResources);
        //}
    }
}
