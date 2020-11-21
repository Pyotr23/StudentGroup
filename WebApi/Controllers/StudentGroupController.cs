using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using StudentGroup.Infrastracture.Shared.Managers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StudentGroup.Services.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentGroupController : ControllerBase
    {
        private readonly ISchoolManager _schoolManager;
        
        public StudentGroupController(ISchoolManager schoolManager)
        {
            _schoolManager = schoolManager;
        }

        //// GET: api/<StudentGroupController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<StudentGroupController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<StudentGroupController>
        [HttpPost("Student")]
        public string Post([FromBody] string value)
        {
            return string.Empty;
        }

        //// PUT api/<StudentGroupController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<StudentGroupController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
