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
        private readonly IGroupsService _groupService;
        private readonly IMapper _mapper;

        /// <summary>
        ///     Конструктор контроллера Student.
        /// </summary>
        /// <param name="studentService"> Сервис для управления студентами. </param>
        /// <param name="groupService"> Сурвис для управления группами. </param>
        /// <param name="mapper"> Интерфейс маппера. </param>
        public StudentsController(IStudentsService studentService, IGroupsService groupService, IMapper mapper)
        {
            _studentService = studentService;
            _groupService = groupService;
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

        /// <summary>
        ///     Добавить студента в группу.
        /// </summary>
        /// <param name="studentId"> Идентификатор студента. </param>
        /// <param name="groupId"> Идентификатор группы. </param>
        /// <returns></returns>
        [HttpPut("{studentId}/Groups/{groupId}")]
        public async Task<ActionResult<FullStudentResource>> AddStudentToGroup(
            int studentId, int groupId)
        {
            if (studentId <= 0 || groupId <= 0)
                return BadRequest();

            var studentDto = await _studentService.GetStudentByIdAsync(studentId);
            if (studentDto == null)
                return NotFound();

            var groupDto = await _groupService.GetGroupByIdAsync(groupId);
            if (groupDto == null)
                return NotFound();

            await _studentService.AddStudentToGroupAsync(studentId, groupDto);

            var fullStudentDto = await _studentService.GetFullStudentInfoAsync(studentId);
            var resource = _mapper.Map<FullStudentResource>(fullStudentDto);
            return Ok(resource);
        }

        /// <summary>
        ///     Убрать студента из группы.
        /// </summary>
        /// <param name="studentId"> Идентификатор студента. </param>
        /// <param name="groupId"> Идентификатор группы. </param>
        /// <returns></returns>
        [HttpDelete("{studentId}/Groups/{groupId}")]
        public async Task<ActionResult<FullStudentResource>> RemoveStudentFromGroup(
            int studentId, int groupId)
        {
            if (studentId <= 0 || groupId <= 0)
                return BadRequest();

            var studentDto = await _studentService.GetStudentByIdAsync(studentId);
            if (studentDto == null)
                return NotFound();

            var groupDto = await _groupService.GetGroupByIdAsync(groupId);
            if (groupDto == null)
                return NotFound();

            await _studentService.DeleteStudentFromGroupAsync(studentId, groupId);

            var fullStudentDto = await _studentService.GetFullStudentInfoAsync(studentId);
            var resource = _mapper.Map<FullStudentResource>(fullStudentDto);
            return Ok(resource);
        }
    }
}
