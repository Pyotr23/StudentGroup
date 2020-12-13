using School.Core.DTOes;
using School.Core.Filters;
using School.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace School.Core.Services
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentWithGroupsDto>> GetAllWithGroups(StudentFilter filter);
        Task<Student> GetStudentById(int id);
        Task<Student> CreateStudent(Student newStudent);
        Task UpdateStudent(Student studentToBeUpdated, Student student);
        Task DeleteStudent(Student student);
    }
}
