using School.Core.DTOes;
using School.Core.Models;
using System.Threading.Tasks;

namespace School.Core.Repositories
{
    public interface IGroupRepository : IBaseRepository<Group>
    {
        Task<Group> GetByIdAsync(int id);
        Task<GroupWithStudentCount> GetGroupWithStudentCountAsync(int id);
    }
}
