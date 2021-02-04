using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using School.Api.Resources.GroupResources;
using School.Core.DTOes;
using School.Core.Filtration.Parameters;
using School.Core.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School.Api.Controllers
{
    /// <summary>
    ///     Контроллер для управления группами.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly IGroupsService _groupService;
        private readonly IStudentsService _studentsService;
        private readonly IMapper _mapper;
        
        public GroupsController(
            IGroupsService groupsService,
            IStudentsService studentsService,
            IMapper mapper)
        {
            _groupService = groupsService;
            _studentsService = studentsService;
            _mapper = mapper;
        }

        /// <summary>
        ///     Получить группу.
        /// </summary>
        /// <param name="id"> Идентификатор группы. </param>
        [HttpGet("{id}")]
        public async Task<ActionResult<GroupResource>> GetGroupById(int id)
        {
            if (id <= 0)
                return BadRequest();

            var groupDto = await _groupService.GetGroupById(id);
            if (groupDto == null)
                return NotFound();

            var groupResource = _mapper.Map<GroupResource>(groupDto);
            return Ok(groupResource);
        }

        /// <summary>
        ///     Создать новую группу.
        /// </summary>
        /// <param name="saveGroupResource"> Характеристики новой группы. </param>
        [HttpPost]
        public async Task<ActionResult<FullGroupResource>> CreateGroup([FromBody] SaveGroupResource saveGroupResource)
        {
            var groupDtoToCreate = _mapper.Map<GroupDto>(saveGroupResource);
            var newGroupDto = await _groupService.CreateGroup(groupDtoToCreate);
            var groupDto = await _groupService.GetGroupById(newGroupDto.Id);
            var groupResource = _mapper.Map<GroupResource>(groupDto);
            return Ok(groupResource);
        }

        /// <summary>
        ///     Обновить информацию о группе.
        /// </summary>
        /// <param name="id"> Идентификатор обновляемой группы. </param>
        /// <param name="saveGroupResource"> Новое описание группы. </param>
        //[HttpPut("{id}")]
        //public async Task<ActionResult<GroupResource>> UpdateGroup(int id,
        //    [FromBody] SaveGroupResource saveGroupResource)
        //{
        //    var validator = new SaveGroupResourceValidator();
        //    var validationResult = await validator.ValidateAsync(saveGroupResource);
        //    var isValidRequest = id > 0 && validationResult.IsValid;
        //    if (!isValidRequest)
        //        return BadRequest();

        //    var groupForUpdate = await _groupService.GetGroupById(id);
        //    if (groupForUpdate == null)
        //        return NotFound();

        //    var group = _mapper.Map<Group>(saveGroupResource);
        //    await _groupService.UpdateGroup(groupForUpdate, group);

        //    var updatedGroup = await _groupService.GetGroupById(id);
        //    var updatedGroupResource = _mapper.Map<GroupResource>(updatedGroup);
        //    return Ok(updatedGroupResource);
        //}

        /// <summary>
        ///     Удалить группу.
        /// </summary>
        /// <param name="id"> Идентификатор группы. </param>
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteGroup(int id)
        //{
        //    if (id <= 0)
        //        return BadRequest();

        //    var group = await _groupService.GetGroupById(id);
        //    if (group == null)
        //        return NotFound();

        //    await _groupService.DeleteGroup(group);
        //    return NoContent();
        //}

        /// <summary>
        ///     Получить группы с возможностью фильтрации. 
        /// </summary>
        /// <param name="filterParameters"> Параметры фильтрации. </param>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FullGroupResource>>> GetGroups(
            [FromQuery] GroupFilterParameters filterParameters)
        {
            var groupDtoes = await _groupService.GetAll(filterParameters);
            var groupResources = groupDtoes
                .Select(dto => _mapper.Map<FullGroupResource>(dto))
                .ToList();
            return Ok(groupResources);
        }

        /// <summary>
        ///     Добавить студента в группу.
        /// </summary>
        /// <param name="groupId"> Идентификатор группы. </param>
        /// <param name="studentId"> Идентификатор студента. </param>
        //[HttpPut("{groupId}/Student/{studentId}")]
        //public async Task<ActionResult<StudentGroupResource>> AddStudentToGroup(
        //    int groupId, int studentId)
        //{
        //    var isValidRequest = studentId > 0 && groupId > 0;
        //    if (!isValidRequest)
        //        return BadRequest();

        //    var studentDto = await _studentsService.GetStudentById(studentId);
        //    var group = await _groupService.GetGroupById(groupId);
        //    if (studentDto == null || group == null)
        //        return NotFound();

        //    var studentGroup = new StudentGroup
        //    {
        //        StudentId = studentDto.Id,
        //        Student = studentDto,
        //        GroupId = group.Id,
        //        Group = group
        //    };

        //    await _studentGroupsService.AddStudentToGroup(studentGroup);
        //    var fullStudentDto = await _studentsService.GetWithGroupNames(studentId);
        //    var groupDto = await _groupService.GetWithStudentCount(groupId);
        //    var studentGroupResource = new StudentGroupResource
        //    {
        //        Student = _mapper.Map<StudentResource>(fullStudentDto),
        //        Group = _mapper.Map<GroupResource>(groupDto)
        //    };
        //    return Ok(studentGroupResource);
        //}
    }
}
