using Microsoft.AspNetCore.Mvc;
using StudentGroup.Infrastracture.Shared.Managers;

namespace StudentGroup.Services.Api.Controllers
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

        [HttpGet("Index")]
        public string Index()
        {
            return "asasfds";
        }
    }
}
