using School.Core.DTOes;
using School.Core.Filtration.Parameters;
using School.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace School.Core.Repositories
{
    public interface IStudentRepository : IRepository<Student>
    {
        Task<IEnumerable<Student>> GetStudentsAsync(StudentFilterParameters filterParameters);
        Task<Student> GetStudentWithGroupsByIdAsync(int id);
        Task<bool> IsUniqueNicknameAsync(string nickname);
    }
}
