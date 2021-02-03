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
        Task DeleteStudentAsync(StudentDto studentDto);
        Task UpdateStudentAsync(StudentDto studentDtoForUpdate, StudentDto studentDto);
        Task<bool> IsUniqueNicknameAsync(string nickname);
    }
}
