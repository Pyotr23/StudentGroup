using StudentGroup.Infrastracture.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentGroup.Infrastracture.Data.Repositories
{
    public interface ISchoolRepository
    {
        Task<Student> AddStudentAsync(Student student);
        Task<IEnumerable<StudentWithGroupIds>> GetStudentsWithGroupIdsAsync();
        Task<IEnumerable<Group>> GetGroupsAsync();
        Task<Student> FindAsync(int id);
    }
}
