using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudentGroup.Infrastracture.Shared.Managers;
using StudentGroup.Services.Api.Models;

namespace StudentGroup.Services.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private ISchoolManager _schoolManager;

        public StudentsController(ISchoolManager schoolManager)
        {
            _schoolManager = schoolManager;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentWithGroups>>> GetAllStudents()
        {
            var studentsWithGroups = await _schoolManager.GetAllStudentsWithGroups();
            var studentsWithGroupNames = studentsWithGroups.Select(s => new StudentWithGroups 
            {
                Id = s.Id,
                Surname = s.Surname,
                Name = s.Name,
                MiddleName = s.MiddleName,
                Nickname = s.Nickname,
                GroupNames = string.Join(", ", s.Groups.Select(g => g.Name))
            });
            return Ok(studentsWithGroupNames);
        }

        //// GET api/<StudentsController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<StudentsController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<StudentsController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<StudentsController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
