using School.Core.DTOes;
using School.Core.Filtration.Parameters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace School.Core.Services
{
    public interface IStudentsService
    {
        Task<IEnumerable<FullStudentDto>> GetStudentsWithGroupNamesAsync(StudentFilterParameters filter);
        Task<StudentDto> GetStudentByIdAsync(int id);
        Task<StudentDto> CreateStudentAsync(StudentDto newStudentDto);
        Task UpdateStudentAsync(int id, StudentDto studentDto);
        Task DeleteStudentAsync(StudentDto studentDto);
        Task<FullStudentDto> GetWithGroupNamesAsync(int id);
        Task<bool> IsUniqueNicknameAsync(string nickname);
        Task<bool> IsUniqueNicknameAsync(string nickname, int id);
        Task<IEnumerable<StudentDto>> GetAllStudentsAsync();
    }
}
