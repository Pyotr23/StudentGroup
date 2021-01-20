using School.Core.Models;
using System.Threading.Tasks;

namespace School.Core.Repositories
{
    public interface IStudentGroupRepository : IBaseRepository<StudentGroup>
    {
        Task<StudentGroup> GetByIdes(int studentId, int groupId);
    }
}
