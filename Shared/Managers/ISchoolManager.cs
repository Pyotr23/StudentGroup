using StudentGroup.Infrastracture.Data.Models;
using StudentGroup.Infrastracture.Shared.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentGroup.Infrastracture.Shared.Managers
{
    public interface ISchoolManager
    {
        StudentDto PostStudent(AddStudentDto addStudentDto);
        Task<IEnumerable<StudentWithGroups>> GetAllStudentsWithGroups();
    }
}
