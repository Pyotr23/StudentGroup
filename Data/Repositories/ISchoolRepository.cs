using StudentGroup.Infrastracture.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentGroup.Infrastracture.Data.Repositories
{
    public interface ISchoolRepository
    {
        Student PostStudent(Student student);
        Task<IEnumerable<StudentWithGroups>> GetStudentsWithGroupsAsync();
    }
}
