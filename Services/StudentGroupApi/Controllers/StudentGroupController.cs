using Microsoft.AspNetCore.Mvc;

namespace StudentGroup.Services.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentGroupController : ControllerBase
    {
        [HttpGet("Index")]
        public string Index()
        {
            return "asasfds";
        }
    }
}
