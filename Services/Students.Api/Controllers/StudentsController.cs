using Microsoft.AspNetCore.Mvc;

namespace Students.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        [HttpGet("Index")]
        public string Index()
        {
            return "asasfds";
        }
    }
}
