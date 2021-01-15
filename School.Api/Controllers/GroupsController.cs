using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using School.Api.Resources;
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
    }
}
