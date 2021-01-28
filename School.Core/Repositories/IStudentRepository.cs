using School.Core.DTOes;
using School.Core.Filtration.Parameters;
using School.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace School.Core.Repositories
{
    public interface IStudentRepository : IRepository<Student>
    {
        Task<int?> GetIdByNicknameAsync(string nickname);
        Task<IEnumerable<StudentWithGroupName>> GetStudentsWithGroupNamesAsync(StudentFilterParameters filterParameters);
        Task<bool> IsUniqueNickname(string nickname);
    }
}
