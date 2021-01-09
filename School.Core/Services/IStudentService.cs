using School.Core.DTOes;
using School.Core.Filtration.Parameters;
using School.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace School.Core.Services
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentWithGroupsDto>> GetAllWithGroupNames(StudentFilterParameters filter);
        Task<Student> GetStudentById(int id);
        Task<Student> CreateStudent(Student newStudent);
        Task UpdateStudent(Student studentToBeUpdated, Student student);
        Task DeleteStudent(Student student);
        Task<StudentWithGroupsDto> GetWithGroupNames(int id);
        Task<bool> IsNullOrUniqueNickname(string nickname);
    }
}
