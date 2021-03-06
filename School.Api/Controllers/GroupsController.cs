﻿using AutoMapper;
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
        private readonly IMapper _mapper;
        
        public GroupsController(
            IGroupsService groupsService,
            IMapper mapper)
        {
            _groupService = groupsService;
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

            var groupDto = await _groupService.GetGroupByIdAsync(id);
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
            var newGroupDto = await _groupService.CreateGroupAsync(groupDtoToCreate);
            var groupDto = await _groupService.GetGroupByIdAsync(newGroupDto.Id);
            var groupResource = _mapper.Map<GroupResource>(groupDto);
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
            if (id <= 0)
                return BadRequest();

            var groupDtoForUpdate = await _groupService.GetGroupByIdAsync(id);
            if (groupDtoForUpdate == null)
                return NotFound();

            var groupDto = _mapper.Map<GroupDto>(saveGroupResource);
            await _groupService.UpdateGroupAsync(groupDtoForUpdate, groupDto);

            var updatedGroup = await _groupService.GetGroupByIdAsync(id);
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

            var groupDto = await _groupService.GetGroupByIdAsync(id);
            if (groupDto == null)
                return NotFound();

            await _groupService.DeleteGroupAsync(groupDto);
            return NoContent();
        }

        /// <summary>
        ///     Получить группы с возможностью фильтрации. 
        /// </summary>
        /// <param name="filterParameters"> Параметры фильтрации. </param>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FullGroupResource>>> GetGroups(
            [FromQuery] GroupFilterParameters filterParameters)
        {
            var groupDtoes = await _groupService.GetAllAsync(filterParameters);
            var groupResources = groupDtoes
                .Select(dto => _mapper.Map<FullGroupResource>(dto))
                .ToList();
            return Ok(groupResources);
        }        
    }
}
