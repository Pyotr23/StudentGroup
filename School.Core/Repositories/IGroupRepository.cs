using School.Core.DTOes;
using School.Core.Filtration.Parameters;
using School.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace School.Core.Repositories
{
    public interface IGroupRepository : IBaseRepository<Group>
    {
        Task<Group> GetByIdAsync(int id);
        Task<IEnumerable<GroupWithStudentCount>> GetAllGroups(GroupFilterParameters filterParameters);
        Task<GroupWithStudentCount> GetGroupWithStudentCountAsync(int id);
    }
}
