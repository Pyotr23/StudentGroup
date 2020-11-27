using Microsoft.AspNetCore.Mvc;
using StudentGroup.Infrastracture.Data.Models;
using StudentGroup.Infrastracture.Shared.Dto;
using StudentGroup.Infrastracture.Shared.Managers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentGroup.Services.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private ISchoolManager _schoolManager;

        public GroupsController(ISchoolManager schoolManager)
        {
            _schoolManager = schoolManager;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var group = await _schoolManager.GetGroup(id);
            if (group == null)
                return NotFound();
            return Ok(group);
        }

        [HttpPost]
        public async Task<ActionResult<Group>> Post([FromBody] Group group)
        {
            var newGroup = await _schoolManager.PostGroup(group);
            return CreatedAtAction("Get", new { id = newGroup.Id }, newGroup);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Group>> Delete(int id)
        {
            var group = await _schoolManager.GetGroup(id);
            if (group == null)
                return NotFound();
            await _schoolManager.RemoveGroup(group);
            return Ok(group);
        }        

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] GroupDto groupDto)
        {
            var group = await _schoolManager.GetGroup(id);
            if (group == null)
                return NotFound();
            group.Name = groupDto.Name;
            await _schoolManager.UpdateGroup(group);
            return NoContent();
        }

        [HttpPut("{groupId}/Students/{studentId}")]
        public async Task<IActionResult> PutStudent(int groupId, int studentId)
        {
            var group = await _schoolManager.GetGroup(groupId);
            if (group == null)
                return NotFound();

            var student = await _schoolManager.GetStudent(studentId);
            if (student == null)
                return NotFound();

            await _schoolManager.AddStudentToGroup(groupId, studentId);
            return NoContent();
        }

        [HttpDelete("{groupId}/Students/{studentId}")]
        public async Task<IActionResult> DeleteStudent(int groupId, int studentId)
        {
            var groupStudent = await _schoolManager.GetGroupStudent(groupId, studentId);
            if (groupStudent == null)
                return NotFound();
                        
            await _schoolManager.RemoveGroupStudent(groupStudent);
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetGroups([FromQuery] string name)
        {
            var groups = await _schoolManager.GetAllGroupsWithStudentCount();
            return Ok(groups);
        }
    }
}
