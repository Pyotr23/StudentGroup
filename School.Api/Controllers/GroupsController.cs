using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using School.Api.Resources;
using School.Api.Validators;
using School.Core.Filtration.Parameters;
using School.Core.Models;
using School.Core.Services;
using System;
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
        private readonly IMapper _mapper;
        
        public GroupsController(IGroupsService groupsService, IMapper mapper)
        {
            _groupService = groupsService;
            _mapper = mapper;
        }

        /// <summary>
        ///     Получить группу с количеством студентов в ней.
        /// </summary>
        /// <param name="id"> Идентификатор группы. </param>
        [HttpGet("{id}")]
        public async Task<ActionResult<GroupResource>> GetGroupById(int id)
        {
            if (id <= 0)
                return BadRequest();

            var groupDto = await _groupService.GetWithStudentCount(id);
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
        public async Task<ActionResult<GroupResource>> CreateGroup([FromBody] SaveGroupResource saveGroupResource)
        {
            var validator = new SaveGroupResourceValidator();
            var validationResult = await validator.ValidateAsync(saveGroupResource);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);            

            var groupToCreate = _mapper.Map<Group>(saveGroupResource);
            var newGroup = await _groupService.CreateGroup(groupToCreate);
            var group = await _groupService.GetGroupById(newGroup.Id);
            var groupResource = _mapper.Map<GroupResource>(group);
            return Ok(groupResource);
        }

        /// <summary>
        ///     Обновить информацию о группе.
        /// </summary>
        /// <param name="id"> Идентификатор обновляемой группы. </param>
        /// <param name="saveGroupResource"> Новое описание группы. </param>
        [HttpPut("{id}")]
        public async Task<ActionResult<GroupResource>> UpdateGroup(int id,
            [FromBody] SaveGroupResource saveGroupResource)
        {
            var validator = new SaveGroupResourceValidator();
            var validationResult = await validator.ValidateAsync(saveGroupResource);
            var isValidRequest = id > 0 && validationResult.IsValid;
            if (!isValidRequest)
                return BadRequest();

            var groupForUpdate = await _groupService.GetGroupById(id);
            if (groupForUpdate == null)
                return NotFound();

            var group = _mapper.Map<Group>(saveGroupResource);
            await _groupService.UpdateGroup(groupForUpdate, group);

            var updatedGroup = await _groupService.GetGroupById(id);
            var updatedGroupResource = _mapper.Map<GroupResource>(updatedGroup);
            return Ok(updatedGroupResource);
        }

        /// <summary>
        ///     Удалить группу.
        /// </summary>
        /// <param name="id"> Идентификатор группы. </param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroup(int id)
        {
            if (id <= 0)
                return BadRequest();

            var group = await _groupService.GetGroupById(id);
            if (group == null)
                return NotFound();

            await _groupService.DeleteGroup(group);
            return NoContent();
        }

        /// <summary>
        ///     Получить группы с возможностью фильтрации. 
        /// </summary>
        /// <param name="filterParameters"> Параметры фильтрации. </param>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GroupResource>>> GetGroups(
            [FromQuery] GroupFilterParameters filterParameters)
        {
            var groupDtoes = await _groupService.GetAll(filterParameters);
            var groupResources = groupDtoes
                .Select(dto => _mapper.Map<GroupResource>(dto))
                .ToList();
            return Ok(groupResources);
        }
    }
}
