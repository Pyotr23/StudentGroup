using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using School.Api.Resources;
using School.Core.Models;
using School.Core.Services;

namespace School.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;

        public StudentController(IStudentService studentService, IMapper mapper)
        {
            _studentService = studentService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StudentResource>> GetStudentById(int id)
        {
            var student = await _studentService.GetStudentById(id);
            var studentResource = _mapper.Map<Student, StudentResource>(student);
            return Ok(studentResource);
        }
    }
}
