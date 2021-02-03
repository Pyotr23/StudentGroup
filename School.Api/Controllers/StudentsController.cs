using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using School.Api.Resources.StudentResources;
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
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<StudentResource>>> GetAllStudents()
        //{
        //    var dtoes = await _studentService.GetAllStudentsAsync();
        //    var resources = dtoes
        //        .Select(dto => _mapper.Map<StudentResource>(dto))
        //        .ToList();
        //    return Ok(resources);
        //}

        /// <summary>
        ///     Получить студента по идентификатору.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentResource>> GetStudentById(int id)
        {
            if (id <= 0)
                return BadRequest();

            var studentDto = await _studentService.GetStudentByIdAsync(id);
            if (studentDto == null)
                return NotFound();

            var studentResource = _mapper.Map<StudentResource>(studentDto);
            return Ok(studentResource);
        }

        /// <summary>
        ///     Создать студента.
        /// </summary>
        /// <param name="saveStudentResource"> Создаваемый студент. </param>
        [HttpPost]
        public async Task<ActionResult<StudentResource>> CreateStudent(
            [FromBody] SaveStudentResource saveStudentResource)
        {
            var studentDtoToCreate = _mapper.Map<StudentDto>(saveStudentResource);
            var newStudentDto = await _studentService.CreateStudentAsync(studentDtoToCreate);
            var createdStudentDto = await _studentService.GetStudentByIdAsync(newStudentDto.Id);
            var studentResource = _mapper.Map<StudentResource>(createdStudentDto);
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
            var studentDto = _mapper.Map<StudentDto>(saveStudentResource);

            var studentDtoToUpdate = await _studentService.GetStudentByIdAsync(id);
            await _studentService.UpdateStudentAsync(studentDtoToUpdate, studentDto);

            var updatedStudentDto = await _studentService.GetStudentByIdAsync(id);
            if (updatedStudentDto == null)
                return NotFound();

            var updatedStudentResource = _mapper.Map<StudentResource>(updatedStudentDto);
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

            var studentDto = await _studentService.GetStudentByIdAsync(id);
            if (studentDto == null)
                return NotFound();

            await _studentService.DeleteStudentAsync(studentDto);
            return NoContent();
        }

        /// <summary>
        ///     Получить студентов с возможностью фильтрации. 
        /// </summary>
        /// <param name="filterParameters"> Параметры фильтрации. </param>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FullStudentResource>>> GetStudents(
            [FromQuery] StudentFilterParameters filterParameters)
        {
            var studentDtoes = await _studentService.GetStudentsWithGroupNamesAsync(filterParameters);
            var studentResources = studentDtoes
                .Select(dto => _mapper.Map<FullStudentResource>(dto))
                .ToList();
            return Ok(studentResources);
        }
    }
}
