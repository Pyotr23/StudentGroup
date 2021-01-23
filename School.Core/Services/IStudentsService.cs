using School.Core.DTOes;
using School.Core.Filtration.Parameters;
using School.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace School.Core.Services
{
    public interface IStudentsService
    {
        Task<IEnumerable<FullStudentDto>> GetAllWithGroupNames(StudentFilterParameters filter);
        Task<StudentDto> GetStudentById(int id);
        Task<StudentDto> CreateStudent(StudentDto newStudentDto);
        Task UpdateStudent(int id, StudentDto studentDto);
        Task DeleteStudent(StudentDto studentDto);
        Task<FullStudentDto> GetWithGroupNames(int id);
        Task<bool> IsUniqueNickname(string nickname);
        Task<bool> IsUniqueNickname(string nickname, int id);
    }
}
