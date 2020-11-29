using Microsoft.AspNetCore.Mvc;
using StudentGroup.Infrastracture.Data.Models;
using StudentGroup.Infrastracture.Data.Models.Database;
using StudentGroup.Infrastracture.Data.Models.Filtration;
using StudentGroup.Infrastracture.Shared.Dto;
using StudentGroup.Infrastracture.Shared.Extensions;
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


        /// <summary>
        ///     Получение группы.
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Найденная группа</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var group = await _schoolManager.GetGroup(id);
            if (group == null)
                return NotFound();
            return Ok(group);
        }

        /// <summary>
        ///     Добавление группы.
        /// </summary>
        /// <param name="group">Характеристики группы</param>
        /// <returns>Созданная группа.</returns>
        [HttpPost]
        public async Task<ActionResult<Group>> Post([FromBody] AddUpdateGroupRequest group)
        {
            var newGroup = await _schoolManager.PostGroup(group.ToDto());
            return CreatedAtAction("Get", new { id = newGroup.Id }, newGroup);
        }

        /// <summary>
        ///     Удаление группы.
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Удалённая группа.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Group>> Delete(int id)
        {
            var group = await _schoolManager.GetGroup(id);
            if (group == null)
                return NotFound();
            await _schoolManager.RemoveGroup(group);
            return Ok(group);
        }        

        /// <summary>
        ///     Обновить группу.
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="bodyGroup">новые параметры группы</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AddUpdateGroupRequest bodyGroup)
        {
            var group = await _schoolManager.GetGroup(id);
            if (group == null)
                return NotFound();
            group.Name = bodyGroup.Name;
            await _schoolManager.UpdateGroup(group);
            return NoContent();
        }

        /// <summary>
        ///     Добавить в группу студента.
        /// </summary>
        /// <param name="groupId">Идентификатор группы</param>
        /// <param name="studentId">Идентификатор студента</param>
        /// <returns></returns>
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

        /// <summary>
        ///     Удалить студента из группы.
        /// </summary>
        /// <param name="groupId">Идентификатор группы</param>
        /// <param name="studentId">Студент группы</param>
        /// <returns></returns>
        [HttpDelete("{groupId}/Students/{studentId}")]
        public async Task<IActionResult> DeleteStudent(int groupId, int studentId)
        {
            var groupStudent = await _schoolManager.GetGroupStudent(groupId, studentId);
            if (groupStudent == null)
                return NotFound();
                        
            await _schoolManager.RemoveGroupStudent(groupStudent);
            return NoContent();
        }

        /// <summary>
        ///     Получить список групп с возможностью фильтрации по названию группы.
        /// </summary>
        /// <param name="name">Название группы для фильтрации</param>
        /// <returns>Список групп с ID, именем группы и количеством студентов в группе.</returns>
        [HttpGet]
        public async Task<IActionResult> GetGroups([FromQuery] string name)
        {
            var filteringParameters = new GroupFilteringParameters()
            {
                Name = name
            };
            var groups = await _schoolManager.GetAllGroupsWithStudentCount(filteringParameters);
            return Ok(groups);
        }
    }
}
